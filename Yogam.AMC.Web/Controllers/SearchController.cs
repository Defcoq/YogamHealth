using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Yogam.AMC.Data.Models;
using Yogam.AMC.Infrastructure;

namespace Yogam.AMC.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;

        public SearchController(ApplicationDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }


        [HttpPost]
        public ActionResult Index(string searchquery, string fromcontroller)
        {
            switch (fromcontroller)
            {
                case "Products":
                    return RedirectToAction("SearchProductsResult", new { query = searchquery });

                case "Customers":
                    return RedirectToAction("SearchCustomersResult", new { query = searchquery });

                case "Employees":
                    return RedirectToAction("SearchEmployeesResult", new { query = searchquery });
            }
            return View();
        }

        public ActionResult SearchProductsResult(string query)
        {
            ViewBag.SearchQuery = query;
            var results = _context.Products.Where(p => p.ProductName.Contains(query)).ToList();
            return View(results);
        }

        public ActionResult SearchCustomersResult(string query)
        {
            ViewBag.SearchQuery = query;
            var results = _context.Customers.Where(p => p.CompanyName.Contains(query)
                || p.ContactName.Contains(query)
                || p.City.Contains(query)
                || p.Country.Contains(query)).ToList();
            return View(results);
        }

        public ActionResult SearchEmployeesResult(string query)
        {
            ViewBag.SearchQuery = query;
            var results = _context.Employees.Where(p => p.FirstName.Contains(query)
                || p.LastName.Contains(query)
                || p.Notes.Contains(query)).ToList();
            return View(results);
        }
    }
}