using Microsoft.AspNetCore.Mvc;

namespace CMCS.Web.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string role)
        {
            if (role == "Lecturer")
                return RedirectToAction("Index", "Claims");
            else if (role == "Coordinator")
                return RedirectToAction("Index", "Admin");
            else
                return View();
        }
    }
}