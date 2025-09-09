using Microsoft.AspNetCore.Mvc;

namespace CMCS.Web.Controllers
{
    public class DocumentsController : Controller
    {
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                TempData["Message"] = "Document uploaded successfully!";
            }
            return View();
        }
    }
}