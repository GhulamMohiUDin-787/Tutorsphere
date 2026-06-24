using Microsoft.AspNetCore.Mvc;

namespace p1.Controllers
{
    public class TutorsController : Controller
    {
        // GET: /Tutors/Index
        public IActionResult Index()
        {
            return View();
        }
    }
}