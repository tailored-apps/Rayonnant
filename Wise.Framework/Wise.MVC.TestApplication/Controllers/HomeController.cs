using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wise.MVC.TestApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDupa dupa;
        public HomeController(IDupa dupa)
        {
            this.dupa = dupa;
        }
        public ActionResult Index()
        {
            dupa.Sraj();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}