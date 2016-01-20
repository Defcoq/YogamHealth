using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Yogam.AMC.Data.Models;
using Yogam.AMC.Infrastructure;
using Yogam.AMC.Infrastructure.Alerts;
using Yogam.AMC.Web.Models;
using PagedList;

namespace Yogam.AMC.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;

        public ProductsController(ApplicationDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public ActionResult Index(int? page)
        {
            var models = _context.Products.Project().To<ProductViewModel>().OrderBy(p => p.ProductName);
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(models.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Edit(int id)
        {
            var model = _context.Products.FirstOrDefault(p => p.ProductID == id);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(Product form)
        {
            if (ModelState.IsValid)
            {
                var model = _context.Products.SingleOrDefault(i => i.ProductID == form.ProductID);
                model.ProductName = form.ProductName;
                model.UnitPrice = form.UnitPrice;
                model.UnitsInStock = form.UnitsInStock;
                model.UnitsOnOrder = form.UnitsOnOrder;
                model.ReorderLevel = form.ReorderLevel;
                model.Discontinued = form.Discontinued;
                _context.Entry(model).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index").WithSuccess("Product saved.");
            }
            return View(form);
        }
    }
}