using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeaShopMVC.Models;

namespace TeaShopMVC.Controllers
{
    public class OrderController : Controller
    {
        TeaContext db;
        private readonly UserManager<IdentityUser> _userManager;
        public OrderController(TeaContext context, UserManager<IdentityUser> manager)
        {
            db = context;
            _userManager = manager;
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            string cartId = GetCookie();
            if (cartId == null)
                return RedirectToAction("Index", "Home");
            var order = new Order()
            {
                LastName = "Введите фамилию",
                Name = "Введите имя",
                Status = "",
                Date = DateTime.Now,
                TotalPrice = 0
            };
            var orderItems = new List<Item>();
            var goods = db.ShoppingCarts.Where(c => c.CartId == cartId).ToList();
            foreach (var good in goods)
            {
                var tea = db.Tea.Find(good.TeaId);
                if (tea == null) continue;
                if (tea.Amount < good.Quantity)
                {
                    good.Quantity = tea.Amount;
                }
                var orderItem = new Item()
                {
                    TeaId = tea.Id,
                    Quantity = good.Quantity,
                    Tea = tea
                };
                orderItems.Add(orderItem);
                order.TotalPrice += tea.Price * orderItem.Quantity;
                db.Entry(tea).State = EntityState.Modified;
            }
            order.Items = orderItems;
            if (User.Identity.IsAuthenticated)
            {
                var name = HttpContext.User.Identity.Name;
                var user = await _userManager.FindByNameAsync(name);
                var client = db.Clients.Find(user.Id);
                if (client != null)
                {
                    order.ClientId = user.Id;
                    if (client.LastName != null) order.LastName = client.LastName;
                    if (client.Name != null) order.Name = client.Name;
                    if (client.Address != null) order.Address = client.Address;
                    if (client.CurrentDiscount > 0)
                    {
                        var discount = (client.CurrentDiscount * order.TotalPrice) / 100;
                        ViewBag.Cost = order.TotalPrice;
                        ViewBag.Discount = discount;
                        order.TotalPrice -= discount;
                    }
                }
            }
            db.Orders.Add(order);
            db.Items.AddRange(orderItems);
            db.SaveChanges();
            SelectList delivery = new SelectList(new[] { "г. Ярославль" });
            ViewBag.DeliveryMethod = delivery;
            return View(order);
        }

        private string GetCookie()
        {
            string cartId = null;
            if (HttpContext.Request.Cookies.Keys.Count > 0 &&
                HttpContext.Request.Cookies.ContainsKey("cartId"))
            {
                cartId = HttpContext.Request.Cookies["cartId"];
            }

            return cartId;
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id, TotalPrice, Date, LastName, Name, Address, DeliveryMethod, Status, ClientId")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.Status = "подтверждён";
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                //remove cartitems
                string cartId = GetCookie();
                if (cartId != null)
                {
                    var carts = db.ShoppingCarts.Where(c => c.CartId == cartId);
                    db.RemoveRange();
                    db.SaveChanges();
                    HttpContext.Response.Cookies.Delete("cartId");
                }

                if (User.Identity.IsAuthenticated)
                {
                    var name = HttpContext.User.Identity.Name;
                    var user = await _userManager.FindByNameAsync(name);
                    var client = db.Clients.Find(user.Id);
                    bool isnew = false;
                    if (client == null)
                    {
                        client = new Client()
                        {
                            Id = user.Id,
                            OrdersNumber = 0,
                            CurrentDiscount = 0,
                            TotalOrdersCost = 0,
                            ReviewsNumber = 0
                        };
                        isnew = true;
                    }
                    client.Address = order.Address;
                    client.Name = order.Name;
                    client.LastName = order.LastName;
                    client.OrdersNumber++;
                    client.TotalOrdersCost += order.TotalPrice;
                    DiscountCalculator(client);
                    if (isnew)
                    {
                        db.Clients.Add(client);
                        order.ClientId = client.Id;
                        db.Entry(order).State = EntityState.Modified;
                    }
                    else db.Entry(client).State = EntityState.Modified;    
                }
                var items = db.Items.Where(x => x.OrderId == order.Id).ToList();
                foreach (var item in items)
                {
                    var tea = db.Tea.Find(item.TeaId);
                    if (tea == null) continue;
                    tea.Amount -= item.Quantity;
                    db.Entry(tea).State = EntityState.Modified;
                }
                await db.SaveChangesAsync();
                return RedirectToAction("Success");
            }
            return View(order);
        }

        private void DiscountCalculator(Client client)
        {
            client.CurrentDiscount = 3;
            if (client.CurrentDiscount < 10000 && client.CurrentDiscount > 10000)
            {
                client.CurrentDiscount = 5;
            }
            else if (client.CurrentDiscount < 20000)
            {
                client.CurrentDiscount = 8;
            }
            else if (client.CurrentDiscount < 30000)
            {
                client.CurrentDiscount = 15;
            }
        }

        public IActionResult Success()
        {
            ViewBag.Msg = "Ожидайте звонка менеджера для подтверждения заказа.";
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var order = db.Orders.Find(id);
            var items = db.Items.Where(x => x.OrderId == id).ToList();
            //foreach (var item in items)
            //{
            //    var tea = db.Tea.Find(item.TeaId);
            //    if (tea == null) continue;
            //    tea.Amount += item.Quantity;
            //    db.Entry(tea).State = EntityState.Modified;
            //}
            order.Items = items;
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
