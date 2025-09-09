using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CMCS.Web.Data;
using CMCS.Web.Hubs;
using CMCS.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CMCS.Web.Controllers
{ 

    public class ClaimsController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IHubContext<ClaimStatusHub> _hub;

        private static readonly string[] AllowedExtensions = { ".pdf", ".docx", ".xlsx" };
        private const long MaxFileSize = 10 * 1024 * 1024; // 10MB

        public ClaimsController(AppDbContext db, IHubContext<ClaimStatusHub> hub)
        {
            _db = db;
            _hub = hub;
        }

        // List of lecturer claims
        public async Task<IActionResult> Index()
        {
            // Demo: default lecturer Id = 1
            var claims = await _db.Claims
                .Include(c => c.Documents)
                .Where(c => c.LecturerId == 1)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
            return View(claims);
        }

        public IActionResult Create()
        {
            return View(new Claim { LecturerId = 1, Status = ClaimStatus.Pending });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Claim model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please fix the highlighted errors.";
                return View(model);
            }

            model.Status = ClaimStatus.Pending;
            _db.Claims.Add(model);
            _db.ClaimStatusHistories.Add(new ClaimStatusHistory
            {
                Claim = model,
                FromStatus = ClaimStatus.Draft,
                ToStatus = ClaimStatus.Pending,
                ActionedBy = "System",
                Comment = "Initial submission"
            });
            await _db.SaveChangesAsync();

            TempData["Success"] = "Claim submitted successfully.";
            return RedirectToAction(nameof(Details), new { id = model.Id });
        }

        public async Task<IActionResult> Details(int id)
        {
            var claim = await _db.Claims
                .Include(c => c.Lecturer)
                .Include(c => c.Documents)
                .Include(c => c.StatusHistory)
                .FirstOrDefaultAsync(c => c.Id == id && c.LecturerId == 1);

            if (claim == null) return NotFound();

            return View(claim);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(int id)
        {
            var claim = await _db.Claims.Include(c => c.Documents)
                .FirstOrDefaultAsync(c => c.Id == id && c.LecturerId == 1);
            if (claim == null) return NotFound();

            if (Request.Form.Files.Count == 0)
            {
                TempData["Error"] = "Please choose a file to upload.";
                return RedirectToAction(nameof(Details), new { id });
            }

            var file = Request.Form.Files[0];
            if (file.Length <= 0 || file.Length > MaxFileSize)
            {
                TempData["Error"] = "File is empty or exceeds 10 MB.";
                return RedirectToAction(nameof(Details), new { id });
            }

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!AllowedExtensions.Contains(ext))
            {
                TempData["Error"] = "Only PDF, DOCX, and XLSX files are allowed.";
                return RedirectToAction(nameof(Details), new { id });
            }

            var uploadsRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            Directory.CreateDirectory(uploadsRoot);
            var storedName = $"{id}_{Path.GetRandomFileName()}{ext}";
            var fullPath = Path.Combine(uploadsRoot, storedName);
            using (var stream = System.IO.File.Create(fullPath))
            {
                await file.CopyToAsync(stream);
            }

            var doc = new Document
            {
                ClaimId = id,
                OriginalFileName = file.FileName,
                StoredFileName = storedName,
                ContentType = file.ContentType,
                SizeBytes = file.Length
            };

            _db.Documents.Add(doc);
            await _db.SaveChangesAsync();

            TempData["Success"] = "Document uploaded.";
            return RedirectToAction(nameof(Details), new { id });
        }

        // Pretty status page that live-updates
        public async Task<IActionResult> Track(int id)
        {
            var claim = await _db.Claims
                .Include(c => c.StatusHistory)
                .FirstOrDefaultAsync(c => c.Id == id && c.LecturerId == 1);
            if (claim == null) return NotFound();
            return View(claim);
        }
    }
}