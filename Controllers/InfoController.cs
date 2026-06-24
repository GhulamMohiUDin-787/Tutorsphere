using Microsoft.AspNetCore.Mvc;

namespace p1.Controllers
{
    // Handles extra informational pages
    public class InfoController : Controller
    {
        // GET: /Info/About
        public IActionResult About()
        {
            return View();
        }

        // GET: /Info/Contact
        public IActionResult Contact()
        {
            return View();
        }

        // GET: /Info/HowItWorks
        public IActionResult HowItWorks()
        {
            return View();
        }
    }
}