using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using p1.Models;

namespace p1.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly AppDbContext _context;

        public BookingController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Booking/Index
        public IActionResult Index()
        {
            var student = GetCurrentStudent();
            if (student == null)
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Index") });

            return View(new Booking
            {
                FullName = student.FullName,
                Email = student.Email
            });
        }

        // POST: /Booking/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Booking booking)
        {
            var student = GetCurrentStudent();
            if (student == null)
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Index") });

            booking.FullName = student.FullName;
            booking.Email = student.Email;
            booking.StudentId = student.Id;

            if (ModelState.IsValid)
            {
                booking.CreatedAt = DateTime.Now;

                _context.Bookings.Add(booking);
                _context.SaveChanges();

                TempData["Success"] =
                    $"✅ Booking confirmed for {booking.FullName} " +
                    $"on {booking.Date} at {booking.Time}!";

                return RedirectToAction("Index");
            }

            return View(booking);
        }

        private Student? GetCurrentStudent()
        {
            var idClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (idClaim == null || !int.TryParse(idClaim, out var studentId))
                return null;

            return _context.Students.Find(studentId);
        }
    }
}
