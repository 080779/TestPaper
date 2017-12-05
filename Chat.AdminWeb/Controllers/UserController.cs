using Chat.AdminWeb.App_Start;
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
    public class UserController : Controller
    {
        public IUserService userService { get; set; }

        [Permission("list")]
        public ActionResult List(int pageIndex=1)
        {
            UserListModel model = new UserListModel();
            UserSearchResult result = userService.Search(null, null, null, null,null, 0, 20);
            model.Users = result.Users;

            //分页
            Pagination pager = new Pagination();
            pager.CurrentLinkClassName = "curPager";
            pager.MaxPagerCount = 10;
            pager.PageIndex = pageIndex;//这些数据，cshtml不知道，就必须让Action传递给我们
            //对于所有cshtml要用到，但是又获取不到的数据，都由Action来获取，然后放到ViewBag或者Model中传递给cshtml
            pager.PageSize = 20;
            pager.TotalCount = result.TotalCount;
            pager.UrlPattern = "javascript:getPage({pn});";
            if (result.TotalCount <= 20)
            {
                model.Page = "";
            }
            else
            {
                model.Page = pager.GetPagerHtml();
            }
            return View(model);
        }

        [Permission("manager")]
        public ActionResult Search(bool? gender, bool? isWon, DateTime? startTime, DateTime? endTime, string keyWord,int pageIndex=1)
        {
            UserListModel model = new UserListModel();
            UserSearchResult result = userService.Search(gender,isWon, startTime, endTime, keyWord, (pageIndex - 1) * 20, 20);

            Pagination pager = new Pagination();
            pager.CurrentLinkClassName = "curPager";
            pager.MaxPagerCount = 10;
            pager.PageIndex = pageIndex;//这些数据，cshtml不知道，就必须让Action传递给我们
            //对于所有cshtml要用到，但是又获取不到的数据，都由Action来获取，然后放到ViewBag或者Model中传递给cshtml
            pager.PageSize = 20;
            pager.TotalCount = result.TotalCount;
            pager.UrlPattern = "javascript:getPage({pn});";

            model.Users = result.Users;
            if (result.TotalCount <= 20)
            {
                model.Page = "";
            }
            else
            {
                model.Page = pager.GetPagerHtml();
            }

            return Json(new AjaxResult { Status = "success", Data = model });            
        }
    }
}