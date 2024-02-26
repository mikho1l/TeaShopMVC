using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using TeaShopMVC.Models;
using TeaShopMVC.ViewModel;

namespace TeaShopMVC.Controllers
{
    public class HomeController : Controller
    {
        TeaContext db;
        public HomeController(TeaContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        #region MainPageFilters
        public IActionResult GetAllTea()
        {
            var results = db.Tea.ToList();
            var cats = db.Tea.Select(b => b.Type).Distinct().ToList();
            cats.Insert(0, new TeaType() { Name = "все", Id = 0 });
            SelectList slist = new SelectList(cats, "Id", "Name");
            var sortOrderList = SortOrderClass.GetAllSortings();
            sortOrderList.Insert(0, new SortOrderClass { Id = 0, Name = "Без сортировки", SortOrder = SortOrder.NameAsc });
            SelectList sortOrders = new SelectList(sortOrderList, "Id", "Name");
            int minprice = results.Select(t => t.Price).Min();
            int maxprice = results.Select(t => t.Price).Max();
            var searchWords = "";
            var criteria = new FilterCriteria()
            {
                MinPrice = minprice,
                MaxPrice = maxprice,
                SearchWord = searchWords,
                CategoryID = 0,
                SortOrderId = 0,
                MinDefaultPrice = minprice,
                MaxDefaultPrice = maxprice
            };
            var filterModel = new Filter()
            {
                Teas = results,
                Categories = slist,
                Criteria = criteria,
                SortOrder = sortOrders
            };
            return View("DoFilter", filterModel);
        }
        public IActionResult GetFruitTea()
        {
            var results = db.Tea.ToList();
            var cats = db.Tea.Select(b => b.Type).Distinct().ToList();
            results = results.Where(t => t.TypeId == 3).ToList();
            cats.Insert(0, new TeaType() { Name = "все", Id = 0 });
            SelectList slist = new SelectList(cats, "Id", "Name");
            var sortOrderList = SortOrderClass.GetAllSortings();
            sortOrderList.Insert(0, new SortOrderClass { Id = 0, Name = "Без сортировки", SortOrder = SortOrder.NameAsc });
            SelectList sortOrders = new SelectList(sortOrderList, "Id", "Name");
            int minprice = db.Tea.Select(t => t.Price).Min();
            int maxprice = db.Tea.Select(t => t.Price).Max();
            var searchWords = "";
            var criteria = new FilterCriteria()
            {
                MinPrice = minprice,
                MaxPrice = maxprice,
                SearchWord = searchWords,
                CategoryID = 2,
                SortOrderId = 0,
                MinDefaultPrice = minprice,
                MaxDefaultPrice = maxprice
            };
            var filterModel = new Filter()
            {
                Teas = results,
                Categories = slist,
                Criteria = criteria,
                SortOrder = sortOrders
            };
            return View("DoFilter", filterModel);
        }
        public IActionResult GetAromaTea()
        {
            var results = db.Tea.ToList();
            var cats = db.Tea.Select(b => b.Type).Distinct().ToList();
            results = results.Where(t => t.TypeId == 2).ToList();
            cats.Insert(0, new TeaType() { Name = "все", Id = 0 });
            SelectList slist = new SelectList(cats, "Id", "Name");
            var sortOrderList = SortOrderClass.GetAllSortings();
            sortOrderList.Insert(0, new SortOrderClass { Id = 0, Name = "Без сортировки", SortOrder = SortOrder.NameAsc });
            SelectList sortOrders = new SelectList(sortOrderList, "Id", "Name");
            int minprice = db.Tea.Select(t => t.Price).Min();
            int maxprice = db.Tea.Select(t => t.Price).Max();
            var searchWords = "";
            var criteria = new FilterCriteria()
            {
                MinPrice = minprice,
                MaxPrice = maxprice,
                SearchWord = searchWords,
                CategoryID = 2,
                SortOrderId = 0,
                MinDefaultPrice = minprice,
                MaxDefaultPrice = maxprice
            };
            var filterModel = new Filter()
            {
                Teas = results,
                Categories = slist,
                Criteria = criteria,
                SortOrder = sortOrders
            };
            return View("DoFilter", filterModel);
        }
        public IActionResult GetClassicTea()
        {
            var results = db.Tea.ToList();
            var cats = db.Tea.Select(b => b.Type).Distinct().ToList();
            results = results.Where(t => t.TypeId == 1).ToList();
            cats.Insert(0, new TeaType() { Name = "все", Id = 0 });
            SelectList slist = new SelectList(cats, "Id", "Name");
            var sortOrderList = SortOrderClass.GetAllSortings();
            sortOrderList.Insert(0, new SortOrderClass { Id = 0, Name = "Без сортировки", SortOrder = SortOrder.NameAsc });
            SelectList sortOrders = new SelectList(sortOrderList, "Id", "Name");
            int minprice = db.Tea.Select(t => t.Price).Min();
            int maxprice = db.Tea.Select(t => t.Price).Max();
            var searchWords = "";
            var criteria = new FilterCriteria()
            {
                MinPrice = minprice,
                MaxPrice = maxprice,
                SearchWord = searchWords,
                CategoryID = 2,
                SortOrderId = 0,
                MinDefaultPrice = minprice,
                MaxDefaultPrice = maxprice
            };
            var filterModel = new Filter()
            {
                Teas = results,
                Categories = slist,
                Criteria = criteria,
                SortOrder = sortOrders
            };
            return View("DoFilter", filterModel);
        }
        public IActionResult GetHerbalTea()
        {
            var results = db.Tea.ToList();
            var cats = db.Tea.Select(b => b.Type).Distinct().ToList();
            results = results.Where(t => t.TypeId == 4).ToList();
            cats.Insert(0, new TeaType() { Name = "все", Id = 0 });
            SelectList slist = new SelectList(cats, "Id", "Name");
            var sortOrderList = SortOrderClass.GetAllSortings();
            sortOrderList.Insert(0, new SortOrderClass { Id = 0, Name = "Без сортировки", SortOrder = SortOrder.NameAsc });
            SelectList sortOrders = new SelectList(sortOrderList, "Id", "Name");
            int minprice = db.Tea.Select(t => t.Price).Min();
            int maxprice = db.Tea.Select(t => t.Price).Max();
            var searchWords = "";
            var criteria = new FilterCriteria()
            {
                MinPrice = minprice,
                MaxPrice = maxprice,
                SearchWord = searchWords,
                CategoryID = 2,
                SortOrderId = 0,
                MinDefaultPrice = minprice,
                MaxDefaultPrice = maxprice
            };
            var filterModel = new Filter()
            {
                Teas = results,
                Categories = slist,
                Criteria = criteria,
                SortOrder = sortOrders
            };
            return View("DoFilter", filterModel);
        }
        #endregion
        public IActionResult Search(string searchString)
        {
            var results = new List<Tea>();
            if (!string.IsNullOrEmpty(searchString))
            {
                Regex rg = new Regex(@"\w+", RegexOptions.IgnoreCase);
                var matches = rg.Matches(searchString);
                string searchWords = string.Empty;
                foreach (var word in matches)
                {
                    var list = db.Tea.Where(b => b.Description.Contains(word.ToString()) || b.Name.Contains(word.ToString())).Include(b => b.Type).ToList();
                    results.AddRange(list);
                    searchWords += word.ToString() + " ";
                }
                if (results.Count == 0)
                {
                    ViewBag.Msg = "По вашему запросу ничего не найдено";
                    return View("Unsuccess");
                }
                results = (from item in results select item).Distinct().ToList();
                int minprice = db.Tea.Select(t => t.Price).Min();
                int maxprice = db.Tea.Select(t => t.Price).Max();
                var cats = db.Tea.Select(b => b.Type).Distinct().ToList();
                cats.Insert(0, new TeaType() { Name = "все", Id = 0 });
                SelectList slist = new SelectList(cats, "Id", "Name");
                var sortOrderList = SortOrderClass.GetAllSortings();
                sortOrderList.Insert(0, new SortOrderClass { Id = 0, Name = "Без сортировки", SortOrder = SortOrder.NameAsc });
                SelectList sortOrders = new SelectList(sortOrderList, "Id", "Name");
                var criteria = new FilterCriteria()
                {
                    MinPrice = minprice,
                    MaxPrice = maxprice,
                    SearchWord = searchWords,
                    CategoryID = 0,
                    SortOrderId = 0,
                    MinDefaultPrice = minprice,
                    MaxDefaultPrice = maxprice
                };
                var filterModel = new Filter()
                {
                    Teas = results,
                    Categories = slist,
                    Criteria = criteria,
                    SortOrder = sortOrders
                };
                return View("DoFilter", filterModel);
            }
            ViewBag.Msg = "Задан пустой поисковый запрос";
            return View("Unsuccess");
        }

        [HttpPost]
        public IActionResult DoFilter([Bind("MinPrice, MaxPrice, SearchWord, CategoryID, SortOrderId, InStock, MinDefaultPrice, MaxDefaultPrice")] FilterCriteria criteria, SortOrder sortOrder = SortOrder.NameAsc)
        {
            var results = new List<Tea>();
            if (!string.IsNullOrEmpty(criteria.SearchWord))
            {
                foreach (var word in criteria.SearchWord.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    var list = db.Tea.Where(b => b.Description.Contains(word.ToString()) || b.Name.Contains(word.ToString())).Include(b => b.Type).ToList();
                    results.AddRange(list);
                }
            }
            else 
            { 
                results = db.Tea.Include(b => b.Type).ToList();
            }
            results = results.FindAll(t => criteria.InStock ? t.Amount > 0 : true);
            if (criteria.MinPrice > 0 && criteria.MaxPrice > criteria.MinPrice)
                results = results.FindAll(b => b.Price >= criteria.MinPrice && b.Price <= criteria.MaxPrice);
            if (criteria.CategoryID > 0)
                results = results.FindAll(b => b.Type.Id == criteria.CategoryID);
            if(criteria.SortOrderId > 0)
            {
                var sort = SortOrderClass.GetAllSortings().Find(t => t.Id == criteria.SortOrderId).SortOrder;
                switch (sort)
                {
                    case SortOrder.PriceAsc: results = results.OrderBy(t => t.Price).ToList(); break;
                    case SortOrder.PriceDesc: results = results.OrderByDescending(t => t.Price).ToList(); break;
                    default: break;
                }
            }
            var cats = db.Tea.Select(b => b.Type).Distinct().ToList();
            cats.Insert(0, new TeaType() { Name = "все", Id = 0 });
            SelectList slist = new SelectList(cats, "Id", "Name");
            var sortOrderList = SortOrderClass.GetAllSortings();
            sortOrderList.Insert(0, new SortOrderClass { Id = 0, Name = "Без сортировки", SortOrder = SortOrder.NameAsc });
            SelectList sortOrders = new SelectList(sortOrderList, "Id", "Name");

            var filterModel = new Filter()
            {
                Teas = results,
                Categories = slist,
                SortOrder = sortOrders,
                Criteria = criteria
            };

            return View(filterModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}