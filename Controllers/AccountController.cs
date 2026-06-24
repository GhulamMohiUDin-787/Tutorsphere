using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using p1.Models;
using p1.Services;

namespace p1.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Account/Register
        public IActionResult Register(string? returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToLocal(returnUrl);

            ViewData["ReturnUrl"] = returnUrl;
            return View(new RegisterViewModel());
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string? returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToLocal(returnUrl);

            if (!ModelState.IsValid)
                return View(model);

            var email = model.Email.Trim().ToLowerInvariant();
            if (await _context.Students.AnyAsync(s => s.Email == email))
            {
                ModelState.AddModelError(nameof(model.Email), "An account with this email already exists.");
                return View(model);
            }

            var student = new Student
            {
                FullName = model.FullName.Trim(),
                Email = email,
                PasswordHash = PasswordHelper.HashPassword(model.Password),
                CreatedAt = DateTime.Now
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            await SignInStudentAsync(student);
            TempData["Success"] = $"Welcome, {student.FullName}! Your account has been created.";
            return RedirectToLocal(returnUrl);
        }

        // GET: /Account/Login
        public IActionResult Login(string? returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToLocal(returnUrl);

            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToLocal(model.ReturnUrl);

            if (!ModelState.IsValid)
                return View(model);

            var email = model.Email.Trim().ToLowerInvariant();
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Email == email);

            if (student == null || !PasswordHelper.VerifyPassword(model.Password, student.PasswordHash))
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return View(model);
            }

            await SignInStudentAsync(student);
            TempData["Success"] = $"Welcome back, {student.FullName}!";
            return RedirectToLocal(model.ReturnUrl);
        }

        // POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["Success"] = "You have been logged out.";
            return RedirectToAction("Index", "Home");
        }

        private async Task SignInStudentAsync(Student student)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, student.Id.ToString()),
                new(ClaimTypes.Name, student.FullName),
                new(ClaimTypes.Email, student.Email)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
                });
        }

        private IActionResult RedirectToLocal(string? returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }
    }
}
