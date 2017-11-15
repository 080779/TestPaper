using Chat.AdminWeb.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chat.AdminWeb.Controllers
{
    public class ActivityController : Controller
    {
        [Permission("list")]
        public ActionResult List()
        {
            return View();
        }
        [Permission("manager")]
        public ActionResult Add()
        {
            return View();
        }
        [Permission("manager")]
        public ActionResult Edit()
        {
            return View();
        }
        [Permission("manager")]
        public ActionResult Prize()
        {
            return View();
        }
    }
}