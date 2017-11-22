using Chat.DTO.DTO;
using Chat.FrontWeb.Models;
using Chat.FrontWeb.Models.user;
using Chat.IService.Interface;
using Chat.WebCommon;
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
        public IUserService userService { get; set; }
        public ISettingService settingService { get; set; }

        public ActionResult Index()
        {
            ActivityViewModel model = new ActivityViewModel();
            ActivityDTO activity;
            if(activityService.GetByStatus("答题进行中") == null && activityService.GetByStatus("开奖中") ==null)
            {
                if(activityService.GetNew()!=null)
                {
                    activity = activityService.GetNew();
                    model.Id = activity.Id;
                    model.Name = activity.Name;
                    model.Description = activity.Description;
                    model.StartTime = activity.StartTime.ToString("yyyy-MM-dd");
                    model.ExamEndTime = activity.ExamEndTime.ToString("yyyy-MM-dd");
                    model.RewardTime = activity.RewardTime.ToString("yyyy-MM-dd");
                    model.AnswerCount = activity.AnswerCount;
                    model.StatusName = activity.StatusName;
                }
                else
                {
                    model.Id = 0;
                    model.Name = "当前暂无活动";
                    model.Description = "当前暂无活动简介";
                    model.StartTime = "****-**-**";
                    model.ExamEndTime = "****-**-**";
                    model.RewardTime = "****-**-**";
                    model.AnswerCount = 0;
                    model.StatusName = "无活动";
                }
            }
            else if(activityService.GetByStatus("答题进行中") != null)
            {
                activity = activityService.GetByStatus("答题进行中");
                model.Id = activity.Id;
                model.Name = activity.Name;
                model.Description = activity.Description;
                model.StartTime = activity.StartTime.ToString("yyyy-MM-dd");
                model.ExamEndTime = activity.ExamEndTime.ToString("yyyy-MM-dd");
                model.RewardTime = activity.RewardTime.ToString("yyyy-MM-dd");
                model.AnswerCount = activity.AnswerCount;
                model.StatusName = activity.StatusName;
            }
            else if (activityService.GetByStatus("开奖中") != null)
            {
                activity = activityService.GetByStatus("开奖中");
                model.Id = activity.Id;
                model.Name = activity.Name;
                model.Description = activity.Description;
                model.StartTime = activity.StartTime.ToString("yyyy-MM-dd");
                model.ExamEndTime = activity.ExamEndTime.ToString("yyyy-MM-dd");
                model.RewardTime = activity.RewardTime.ToString("yyyy-MM-dd");
                model.AnswerCount = activity.AnswerCount;
                model.StatusName = activity.StatusName;
            }

            activityService.UpdateCount(model.Id, true, false, false, false, false);
            return View(model);
        }

        public ActionResult Judge(long id,string typeName,string statusName)
        {
            if(statusName=="无活动")
            {
                return Json(new AjaxResult { Status = "nonExist", ErrorMsg = "当前暂无活动" });
            }
            if(statusName=="待开始")
            {
                if (typeName == "topic")
                {
                    return Json(new AjaxResult { Status = "redirect", Data = "/home/topic?id=" + id });
                }
                if (typeName == "answer")
                {
                    return Json(new AjaxResult { Status = "tip", ErrorMsg="活动未开始"});
                }
                if (typeName == "prize")
                {
                    return Json(new AjaxResult { Status = "tip", ErrorMsg = "活动未开始" });
                }
            }
            if (statusName == "答题进行中")
            {
                if (typeName == "topic")
                {
                    return Json(new AjaxResult { Status = "redirect", Data = "/home/topic?id=" + id });
                }
                if (typeName == "answer")
                {
                    return Json(new AjaxResult { Status = "redirect", Data = "/home/answer?id=" + id });
                }
                if (typeName == "prize")
                {
                    return Json(new AjaxResult { Status = "redirect", Data = "/home/prize?id=" + id });
                }
            }
            if (statusName == "开奖中")
            {
                if (typeName == "topic")
                {
                    return Json(new AjaxResult { Status = "redirect", Data = "/home/topic?id=" + id });
                }
                if (typeName == "answer")
                {
                    return Json(new AjaxResult { Status = "tip", ErrorMsg = "开奖中" });
                }
                if (typeName == "prize")
                {
                    return Json(new AjaxResult { Status = "redirect", Data = "/home/prize?id=" + id });
                }
            }
            return Json(new AjaxResult { Status="success"});
        }

        public ActionResult Answer(long id)
        {
            AnswerViewModel model = new AnswerViewModel();
            ActivityDTO activity;
            if (!activityService.ExistActivity(id))
            {
                activity = activityService.GetByStatus("答题进行中");
            }
            else
            {

            }
            activity = activityService.GetById(id);
            model.ActivityName = activity.Name;
            model.Exercises= exeService.GetExercisesByPaperId(activity.PaperId);
            model.Id = activity.Id;
            model.ImgUrl = activity.ImgUrl;
            model.FirstUrl= settingService.GetValue("前端奖品图片地址"); 
            return View(model);
        }

        public ActionResult Topic(long id)
        {
            TopicModel model = new TopicModel();
            ActivityDTO activity;
            if (id <= 0)
            {
                activity = activityService.GetByStatus("答题进行中");
            }
            else
            {
                activity = activityService.GetById(id);
            }            
            model.ActivityName = activity.Name;
            var exetips = exeService.GetExercisesByPaperId(activity.PaperId);
            List<string> lists = new List<string>();
            foreach(var exetip in exetips)
            {
                lists.Add(exetip.Tip);
            }
            model.ExesTip = lists;
            return View(model);
        }

        public ActionResult Prize(long id)
        {
            PrizeViewModel model = new PrizeViewModel();
            ActivityDTO activity;
            if (id <= 0)
            {
                activity = activityService.GetByStatus("答题进行中");
            }
            else
            {
                activity = activityService.GetById(id);
            }
            activity = activityService.GetById(id);
            model.ActivityName = activity.Name;
            model.PrizeName = activity.PrizeName;
            model.PrizeImgUrl = activity.PrizeImgUrl;
            model.PrizeTime = activity.RewardTime;
            model.PrizeFirstUrl = settingService.GetValue("前端奖品图片地址");
            model.StatusName = activity.StatusName;
            var users = userService.GetByActivityIdIsWon1(activity.Id);
            List<IsWonUser> winUsers = new List<IsWonUser>();            
            foreach (var user in users)
            {
                IsWonUser winUser = new IsWonUser();
                winUser.UserName = user.Name;
                winUser.Mobile = CommonHelper.FormatMoblie(user.Mobile);
                winUsers.Add(winUser);
            }
            model.Users = winUsers;
            model.winCount = winUsers.Count();
            string mobile = (string)Session["Mobile"];
            model.UserIsWon = userService.UserIsWonByMobile(mobile);
            return View(model);
        }

        public ActionResult Result(string asks,long id)
        {

            if(string.IsNullOrEmpty(asks))
            {
                return Content("请答完题再提交");
            }
            ResultModel model = new ResultModel();
            ActivityDTO activity = activityService.GetById(id);
            model.ActivityName = activity.Name;
            model.Id = activity.Id;
            model.PrizeTime = activity.RewardTime.ToString("yyyy-MM-dd");
            long paperId = activity.PaperId;
            string[] strs = asks.Trim(',').Split(',');
            List<string> lists = new List<string>();
            int count = 0;
            foreach(string str in strs)
            {
                string[] results = str.Split(':');
                bool b = exeService.IsRightOrWrong(paperId, Convert.ToInt64(results[0]), Convert.ToInt64(results[1]));
                if(b)
                {
                    count++;
                }
                lists.Add(b ? "right" : "wrong");
            }
            model.IsAllRight = count == strs.Count();
            model.Result = lists;
            return View(model);
        }

        public ActionResult SaveUser(AddUser model)
        {
            activityService.UpdateCount(model.Id, false, false, true, false, false);
            //if(!ModelState.IsValid)
            //{
            //    return Json(new AjaxResult { Status="error",ErrorMsg=MVCHelper.GetValidMsg(ModelState)});
            //}
            if (string.IsNullOrEmpty(model.Name))
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "姓名不能为空" });
            }
            if (string.IsNullOrEmpty(model.Mobile))
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "手机号不能为空" });
            }
            if (string.IsNullOrEmpty(model.Address))
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "地址不能为空" });
            }
            if (model.Name.Length<2 || model.Name.Length>20)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "姓名长度在2-20之间" });
            }            
            long m;
            if(!long.TryParse(model.Mobile,out m))
            {
                return Json(new AjaxResult { Status="error",ErrorMsg="手机号必须是数字"});
            }
            if (model.Mobile.Length != 11)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "请输入11位手机号" });
            }
            if (model.Address.Length < 2 || model.Address.Length > 20)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "地址长度在2-300之间" });
            }
            long userId= userService.AddNew(model.Name, "", "", model.Mobile, model.Gender, model.Address);
            activityService.AddUserId(model.Id, userId);
            userService.RetSetWon(userId);
            userService.IsHavePrizeChance(userId);            
            if (userId==-1)
            {
                return Json(new AjaxResult { Status = "error",ErrorMsg="你已参加本次活动，无法再次参与！" });
                //userService.UpdateUser(model.Mobile, model.Name, model.Gender, model.Address);                
            }
            if(userId>0)
            {                
                Session["Mobile"] = model.Mobile;
            }
            return Json(new AjaxResult { Status="success"});
        }
    }
}