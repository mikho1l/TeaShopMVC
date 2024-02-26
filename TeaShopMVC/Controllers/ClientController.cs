using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeaShopMVC.Models;

namespace TeaShopMVC.Controllers
{
    public class ClientController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private TeaContext db;
        public ClientController(UserManager<IdentityUser> manager, TeaContext context)
        {
            _userManager = manager;
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            var name = HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(name);
            var client = db.Clients.Where(t => t.Id == user.Id).FirstOrDefault();
            ViewBag.ClientEmail = user.Email;
            return View(client);
        }
        [HttpPost]
        public async Task<IActionResult> Index([Bind("Id,LastName,Name,Address, TotalOrdersCost, OrdersNumber, ReviewsNumber, CurrentDiscount")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Update(client);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "Home");
            }
            return View(client);
        }

        public async Task<IActionResult> GetPurchaseHistory()
        {
            var name = HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(name);
            var orders = db.Orders.Where(d => d.ClientId == user.Id).OrderByDescending(d => d.Date).ToList();
            ViewBag.Orders = orders;
            var books = new List<Tea>();
            foreach (var order in orders)
            {
                var items = db.Items.Where(i => i.OrderId == order.Id).Include(b => b.Tea).ToList();
                books.AddRange(items.Select(item => item.Tea));
            }
            var list = books.Distinct().ToList();
            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> LeaveReview(int id)
        {
            var tea = db.Tea.Find(id);
            var name = HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(name);
            var client = db.Clients.Find(user.Id);//???
            if (tea != null)
            {
                var otziv = new Review()
                {
                    TeaId = id,
                    ClientId = user.Id,
                    Owner = client
                };
                ViewBag.Tea = tea;
                return View(otziv);
            }
            return RedirectToAction("GetPurchaseHistory");
        }
        [HttpPost]
        public async Task<IActionResult> LeaveReview([Bind("ClientId, TeaId, Text")] Review review)
        {
            if (ModelState.IsValid)
            {
                review.Owner = db.Clients.Find(review.ClientId);
                var name = HttpContext.User.Identity.Name;
                var user = await _userManager.FindByNameAsync(name);
                //review.ClientId = user.Id;
                //review.Owner = db.Clients.Find(user.Id);              
                var client = db.Clients.Find(user.Id);
                db.Reviews.Add(review);
                client.ReviewsNumber++;
                db.Entry(client).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("GetPurchaseHistory");
            }
            return await LeaveReview(review.TeaId);
        }
    }
}
