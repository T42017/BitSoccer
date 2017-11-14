using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace bitsoccer.net.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.Title = "Home";
            
            return View();
        }
        
        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Title = "ABOUT / FAQ ";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Title = "Contact";

            return View();
        }
    }
}