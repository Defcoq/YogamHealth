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

namespace Yogam.AMC.Web.Controllers
{
    [Authorize]
    public class CategoriesController : NorthwindController
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;

        public CategoriesController(ApplicationDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public ActionResult Index()
        {
            var models = _context.Categories.Project().To<CategoryViewModel>().ToArray();
            return View(models);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(CategoryViewModel form)
        {
            if (ModelState.IsValid)
            {
                var category = new Category();
                category.CategoryName = form.CategoryName;
                category.Description = form.Description;
                _context.Categories.Add(category);
                _context.SaveChanges();
                return RedirectToAction("Index").WithSuccess("Category created.");
            }
            return View(form);
        }

        public ActionResult Edit(int id)
        {
            var model = _context.Categories.Project().To<CategoryViewModel>().SingleOrDefault(i => i.CategoryID == id);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryViewModel form)
        {
            if (ModelState.IsValid)
            {
                var model = _context.Categories.SingleOrDefault(i => i.CategoryID == form.CategoryID);

                model.CategoryName = form.CategoryName;
                model.Description = form.Description;
                _context.Entry(model).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index").WithSuccess("Category saved.");
            }
            return View(form);
        }

        public ActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);

            if (category == null)
            {
                return RedirectToAction("Index").WithError("Unable to find the category.  Maybe it was deleted?");
            }

            _context.Categories.Remove(category);

            _context.SaveChanges();

            return RedirectToAction("Index").WithSuccess("Category deleted!");
        }

        public PartialViewResult Blah()
        {
            return PartialView("Partial1");
        }
    }
}