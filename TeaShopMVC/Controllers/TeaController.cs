using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TeaShopMVC.Models;

namespace TeaShopMVC.Controllers
{
    public class TeaController : Controller
    {
        TeaContext db;
        Microsoft.AspNetCore.Hosting.IHostingEnvironment environment;
        const int ImageWidth = 150;
        const int ImageHeight = 150;
        public TeaController(TeaContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            db = context;
            environment = env;
        }
        [Authorize(Roles = "admin,manager")]
        public IActionResult Index(SortOrder sortOrder = SortOrder.NameAsc)
        {
            var teas = db.Tea.Include(t => t.Type).ToList();
            ViewData["NameSort"] = sortOrder == SortOrder.NameAsc ? SortOrder.NameDesc : SortOrder.NameAsc;
            ViewData["PriceSort"] = sortOrder == SortOrder.PriceAsc ? SortOrder.PriceDesc : SortOrder.PriceAsc;
            ViewData["WeightSort"] = sortOrder == SortOrder.WeightAsc ? SortOrder.WeightDesc : SortOrder.WeightAsc;
            ViewData["TypeSort"] = sortOrder == SortOrder.TypeAsc ? SortOrder.TypeDesc : SortOrder.TypeAsc;
            ViewData["AmountSort"] = sortOrder == SortOrder.AmountAsc ? SortOrder.AmountDesc : SortOrder.AmountAsc;
            switch (sortOrder)
            {
                case SortOrder.NameAsc: teas = teas.OrderBy(t => t.Name).ToList(); break;
                case SortOrder.NameDesc: teas = teas.OrderByDescending(t => t.Name).ToList(); break;
                case SortOrder.PriceAsc: teas = teas.OrderBy(t => t.Price).ToList(); break;
                case SortOrder.PriceDesc: teas = teas.OrderByDescending(t => t.Price).ToList(); break;
                case SortOrder.WeightAsc: teas = teas.OrderBy(t => t.Weight).ToList(); break;
                case SortOrder.WeightDesc: teas = teas.OrderByDescending(t => t.Weight).ToList(); break;
                case SortOrder.TypeAsc: teas = teas.OrderBy(t => t.Type.Name).ToList(); break;
                case SortOrder.TypeDesc: teas = teas.OrderByDescending(t => t.Type.Name).ToList(); break;
                case SortOrder.AmountAsc: teas = teas.OrderBy(t => t.Amount).ToList(); break;
                case SortOrder.AmountDesc: teas = teas.OrderByDescending(t => t.Amount).ToList(); break;
            }
            return View(teas);
        }
        [Authorize(Roles = "admin,manager")]
        public IActionResult Create()
        {
            ViewData["TypeId"] = new SelectList(db.TeaType, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Price,Weight,TypeId,Amount,Description")] Tea tea, IFormFile upload)
        {
            if (upload != null)
            {
                string fileName = Path.GetFileName(upload.FileName);
                var extFile = fileName.Substring(fileName.Length - 3);
                if (extFile.Contains("png") || extFile.Contains("jpg") ||
                    extFile.Contains("bmp"))
                {
                    var image = Image.Load(upload.OpenReadStream());
                    image.Mutate(x => x.Resize(ImageWidth, ImageHeight));
                    string path = "\\wwwroot\\images\\" + fileName;
                    var rootPath = environment.ContentRootPath;
                    path = rootPath + path;
                    image.Save(path);
                    tea.ImageUrl = fileName;
                }
            }
            TryValidateModel(tea);
            if (ModelState.IsValid)
            {
                db.Add(tea);
                db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeId"] = new SelectList(db.TeaType, "Id", "Name", tea.TypeId);
            return View(tea);
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();
            var book = db.Tea.Include(b => b.Type)
                .FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound();
            var reviews = db.Reviews.Include(c => c.Owner).Where(r => r.TeaId == book.Id).ToList();
            if (reviews == null || reviews.Count == 0)
            {
                ViewBag.HasReview = false;
            }
            else
            {
                ViewBag.HasReview = true;
                ViewBag.Reviews = reviews;
            }
            return View(book);
        }
        [Authorize(Roles = "admin,manager")]
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var tea = db.Tea.Include(b => b.Type)
                .FirstOrDefault(b => b.Id == id);
            if (tea == null)
                return NotFound();
            ViewData["TypeId"] = new SelectList(db.TeaType, "Id", "Name", tea.TypeId);
            return View(tea);
        }
        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,Name,Price,Weight,Description,TypeId,Amount, ImageUrl")] Tea tea, IFormFile upload)
        {
            if (upload != null)
            {
                string fileName = Path.GetFileName(upload.FileName);
                var extFile = fileName.Substring(fileName.Length - 3);
                if (extFile.Contains("png") || extFile.Contains("jpg") ||
                    extFile.Contains("bmp"))
                {
                    var image = Image.Load(upload.OpenReadStream());
                    image.Mutate(x => x.Resize(ImageWidth, ImageHeight));
                    string path = "\\wwwroot\\images\\" + fileName;
                    var rootPath = environment.ContentRootPath;
                    path = rootPath + path;
                    image.Save(path);
                    tea.ImageUrl = fileName;
                }
            }
            if (ModelState.IsValid)
            {
                db.Update(tea);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(db.TeaType, "Id", "Name", tea.TypeId);
            return View(tea);
        }

    }
    
}
