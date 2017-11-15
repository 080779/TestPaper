using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chat.FrontWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Answer()
        {
            return View();
        }

        public ActionResult Topic()
        {
            return View();
        }

        public ActionResult Prize()
        {
            return View();
        }

        public ActionResult Result()
        {
            return View();
        }
    }
}