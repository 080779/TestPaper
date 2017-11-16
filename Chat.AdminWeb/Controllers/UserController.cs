using Chat.AdminWeb.App_Start;
using Chat.DTO.DTO;
using Chat.IService.Interface;
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
    }
}