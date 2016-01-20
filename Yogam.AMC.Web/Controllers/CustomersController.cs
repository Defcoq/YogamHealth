using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Yogam.AMC.Data.Models;
using Yogam.AMC.Infrastructure;
using Yogam.AMC.Infrastructure.Alerts;
using Yogam.AMC.Web.Models;
using PagedList;
using StructureMap.Query;

namespace Yogam.AMC.Web.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;

        public CustomersController(ApplicationDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public ActionResult Index(int? page)
        {
            var model = _context.Customers.Project().To<CustomerViewModel>().OrderBy(p => p.CompanyName);
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var model = _context.Customers.First(c => c.CustomerId == id);
            ViewBag.RegionId = new SelectList(_context.Regions, "RegionId", "RegionDescription", model.RegionId);
            ViewBag.TerritoryId = new SelectList(_context.Territories.Where(t => t.RegionId == model.RegionId), "TerritoryId", "TerritoryDescription", model.TerritoryId);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(Customer form)
        {
            if (ModelState.IsValid)
            {
                var model = _context.Customers.First(c => c.CustomerId == form.CustomerId);
                model.CustomerCode = form.CustomerCode;
                model.CompanyName = form.CompanyName;
                model.ContactName = form.ContactName;
                model.ContactTitle = form.ContactTitle;
                model.Address = form.Address;
                model.City = form.City;
                model.RegionId = form.RegionId;
                model.TerritoryId = form.TerritoryId;
                model.PostalCode = form.PostalCode;
                model.Country = form.Country;
                _context.Entry(model).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index").WithSuccess("Customer saved.");
            }
            ViewBag.RegionId = new SelectList(_context.Regions, "RegionId", "RegionDescription", form.RegionId);
            ViewBag.TerritoryId = new SelectList(_context.Territories.Where(t => t.RegionId == form.RegionId), "TerritoryId", "TerritoryDescription", form.TerritoryId);
            return View(form);
        }

        public PartialViewResult Territories(int regionId)
        {
            ViewBag.TerritoryId = new SelectList(_context.Territories.Where(t => t.RegionId == regionId), "TerritoryId", "TerritoryDescription");
            return PartialView();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int customerId)
        {
            var model = _context.Customers.FirstOrDefault(i => i.CustomerId == customerId);
            if (model != null)
            {
                _context.Customers.Remove(model);
                _context.SaveChanges();
                return RedirectToAction("Index").WithSuccess("Customer (" + model.CompanyName + ") deleted.");
            }
            return RedirectToAction("Index").WithError("Customer was not found.");
        }
    }
}