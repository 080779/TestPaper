using Chat.IService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chat.FrontWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActivityService activityService { get; set; }
        public IExercisesService exeService { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Answer(long paperId)
        {
            var dto= exeService.GetExercisesByPaperId(paperId);
            return View(dto);
        }

        public ActionResult Topic(long paperId)
        {
            var dto = exeService.GetExercisesByPaperId(paperId);
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