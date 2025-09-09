using Microsoft.AspNetCore.Mvc;
using CMCS.Web.Models;

namespace CMCS.Web.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            // later: load claims from DB
            return View();
        }

        [HttpPost]
        public IActionResult Approve(int id)
        {
            // TODO: update claim in DB
            TempData["Message"] = $"Claim {id} approved ✅";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Reject(int id)
        {
            // TODO: update claim in DB
            TempData["Message"] = $"Claim {id} rejected ❌";
            return RedirectToAction("Index");
        }
    }
}