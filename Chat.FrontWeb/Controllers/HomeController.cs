using Chat.DTO.DTO;
using Chat.FrontWeb.Models;
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
            ActivityViewModel model = new ActivityViewModel();
            model.Activity= activityService.GetByStatus("答题进行中");
            return View(model);
        }

        public ActionResult Answer()
        {
            AnswerViewModel model = new AnswerViewModel();
            ActivityDTO activity = activityService.GetByStatus("答题进行中");
            model.ActivityName = activity.Name;
            model.Exercises= exeService.GetExercisesByPaperId(activity.PaperId);
            return View(model);
        }

        public ActionResult Topic()
        {
            return View();
        }

        public ActionResult Prize()
        {
            PrizeViewModel model = new PrizeViewModel();
            ActivityDTO activity= activityService.GetByStatus("答题进行中");
            model.ActivityName = activity.Name;
            model.PrizeName = activity.PrizeName;
            model.PrizeImgUrl = activity.PrizeImgUrl;
            return View(model);
        }

        public ActionResult Result()
        {
            return View();
        }
    }
}