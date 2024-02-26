using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeaShopMVC.Models;

namespace TeaShopMVC.Controllers
{
    public class CartController : Controller
    {
        TeaContext db;
        public CartController(TeaContext context)
        {
            db = context;
        }
        public IActionResult Add(int id)
        {
            string cartId;
            if (HttpContext.Request.Cookies.Keys.Count > 0 &&
                HttpContext.Request.Cookies.ContainsKey("cartId"))
            {
                cartId = HttpContext.Request.Cookies["cartId"];
            }
            else
            {
                cartId = Guid.NewGuid().ToString();
                HttpContext.Response.Cookies.Append("cartId", cartId);
            }
            var query = db.ShoppingCarts.Where(c => c.CartId == cartId
                        && c.TeaId == id);
            if (query.Any())
            {
                CartItem cartItem = query.First();
                cartItem.Quantity++;
                db.Entry(cartItem).State = EntityState.Modified;
            }
            else
            {
                var item = new CartItem()
                {
                    TeaId = id,
                    CartId = cartId,
                    Quantity = 1
                };
                db.ShoppingCarts.Add(item);
            }
            db.SaveChanges();
            return View("Close");
            //return new EmptyResult();
            //return RedirectToAction("Search", "Home");
            
        }


        public IActionResult Index()
        {
            string cartId = null;
            if (HttpContext.Request.Cookies.Keys.Count > 0 &&
                HttpContext.Request.Cookies.ContainsKey("cartId"))
            {
                cartId = HttpContext.Request.Cookies["cartId"];
            }
            List<CartItem> cartList = new List<CartItem>();
            if (cartId != null)
            {
                cartList = db.ShoppingCarts.Where(c => c.CartId == cartId).ToList();
                int sum = 0;
                foreach (var item in cartList)
                {
                    var tea = db.Tea.Find(item.TeaId);
                    item.SelectedTea = tea;
                    sum += tea.Price * item.Quantity;
                }
                ViewBag.Sum = sum;

            }
            if (cartList.Count > 0)
            {
                ViewBag.Msg = "Ваш чай:";
            }
            else
            {
                ViewBag.Msg = "Ваша корзина пуста";

            }
            return View(cartList);
        }
        public IActionResult Delete(int id)
        {
            var cartItem = db.ShoppingCarts.Find(id);
            db.ShoppingCarts.Remove(cartItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public class ChangeItemQuantityDto
        {
            public int id { get; set; }
            public int newQuantity { get; set; }
        }
        public class CartChangingResult
        {
            public int delta { get; set; }
            public int cartCount { get; set; }
            public int teaId { get; set; }
        }
        [HttpPost]
        public IActionResult ChangeItemQuantity([FromBody] ChangeItemQuantityDto dto)
        {
            var cartItem = db.ShoppingCarts.Find(dto.id);
            var tea = db.Tea.Find(cartItem.TeaId);

            var delta = (dto.newQuantity - cartItem.Quantity) * tea.Price;
            cartItem.Quantity = dto.newQuantity;
            db.Entry(cartItem).State = EntityState.Modified;
            db.SaveChanges();
            int count = db.ShoppingCarts
                .Where(c => c.CartId == cartItem.CartId)
                .Sum(c => c.Quantity);

            return Json(new CartChangingResult() { delta = delta, cartCount = count, teaId = tea.Id });
        }
    }
}
