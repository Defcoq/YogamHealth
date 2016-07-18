using AutoMapper.QueryableExtensions;
using System.Web.Mvc;
using System.Linq;
using Yogam.AMC.Data.Models;
using Yogam.AMC.Infrastructure;
using Yogam.AMC.Web.Models;

namespace Yogam.AMC.Web.Controllers
{
    public class HomeController : NorthwindController
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;

        public HomeController(ApplicationDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Title = "About";
            //var models = _context.Employees.Project().To<EmployeeViewModel>().ToArray();
           // return View(models);
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult FAQ()
        {
            return View();
        }

        public ActionResult SanRaffaelle()
        {
            return View();
        }
    }
}