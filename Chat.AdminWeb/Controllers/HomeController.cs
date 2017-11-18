using Chat.AdminWeb.Models;
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
    public class HomeController : Controller
    {
        public IAdminUserService adminService { get; set; }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AdminUserLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = MVCHelper.GetValidMsg(ModelState) });
            }
            if (adminService.CheckLogin(model.Name, model.Password))
            {
                Session["AdminUserId"] = adminService.GetByName(model.Name).Id;
                return Json(new AjaxResult { Status = "redirect",Data="/testpaper/list" });
            }
            else
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "用户名密码错误" });
            }
        }

        public ActionResult Logout()
        {
            Session["AdminUserId"] = null;
            return Redirect("/home/login");
        }

        public ActionResult LoadManager()
        {
            long? id = (long?)Session["AdminUserId"];
            if(id==null)
            {
                id = 0;
            }
            AdminUserDTO dto= adminService.GetById((long)id);
            return Json(new AjaxResult { Status="success",Data=dto.Name});
        }
    }
}