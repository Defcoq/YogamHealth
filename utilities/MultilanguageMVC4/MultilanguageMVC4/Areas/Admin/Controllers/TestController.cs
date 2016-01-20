using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultilanguageMVC4.Areas.Admin.Controllers
{
    public class TestController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}
