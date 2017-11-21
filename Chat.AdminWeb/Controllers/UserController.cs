using Chat.AdminWeb.App_Start;
using Chat.DTO.DTO;
using Chat.IService.Interface;
using Chat.WebCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chat.AdminWeb.Controllers
{
    public class UserController : Controller
    {
        public IUserService userService { get; set; }

        [Permission("list")]
        public ActionResult List()
        {
            UserDTO[] dtos= userService.GetAll();
            return View(dtos);
        }
        [Permission("manager")]
        public ActionResult Search(bool? gender, bool? isWon, DateTime? startTime, DateTime? endTime, string keyWord)
        {
            return Json(new AjaxResult { Status = "success", Data = userService.Search(gender,isWon, startTime, endTime, keyWord, 0, 10) });
        }
    }
}