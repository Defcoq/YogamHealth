using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Yogam.AMC.Data.Models;
using Yogam.AMC.Infrastructure;
using Yogam.AMC.Web.Models;

namespace Yogam.AMC.Web.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;

        public EmployeesController(ApplicationDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public ActionResult Index()
        {
            var models = _context.Employees.Project().To<EmployeeViewModel>();
            return View(models);
        }

        public ActionResult Edit(int id)
        {
            var model = _context.Employees.First(e => e.EmployeeID == id);
            return View(model);
        }
    }
}