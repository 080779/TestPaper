using Chat.AdminWeb.App_Start;
using Chat.AdminWeb.Models;
using Chat.DTO.DTO;
using Chat.IService.Interface;
using Chat.WebCommon;
using CodeCarvings.Piczard;
using CodeCarvings.Piczard.Filters.Watermarks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chat.AdminWeb.Controllers
{
    public class ActivityController : Controller
    {
        public IActivityService activityService { get; set; }
        public ITestPaperService paperService { get; set; }
        public IIdNameService idNameService { get; set; }

        [Permission("list")]
        public ActionResult List()
        {
            ActivityDTO[] dtos = activityService.GetAll();
            return View(dtos);
        }
        [Permission("manager")]
        public ActionResult Add()
        {
            ActivityAddLoadModel model = new ActivityAddLoadModel();
            model.Papers = paperService.GetAll();
            model.Status = idNameService.GetAll("活动状态");
            return View(model);
        }
        [HttpPost]
        [Permission("manager")]
        public ActionResult Add(AtivityAddModel model)
        {
            if(string.IsNullOrEmpty(model.Name))
            {
                return Content("活动名不能为空");
            }
            if (string.IsNullOrEmpty(model.Description))
            {
                return Content("活动简介不能为空");
            }
            if(model.StatusId<=0)
            {
                return Content("活动状态必须选择");
            }
            if (model.imgUrl==null)
            {
                return Content("活动背景图不能为空");
            }
            if (model.StartTime== Convert.ToDateTime("0001-1-1 0:00:00"))
            {
                return Content("活动开始时间不能为空");
            }
            if (model.ExamEndTime== Convert.ToDateTime("0001-1-1 0:00:00"))
            {
                return Content("答题截止时间不能为空");
            }
            if (model.RewardTime== Convert.ToDateTime("0001-1-1 0:00:00"))
            {
                return Content("开奖时间不能为空");
            }
            if (model.PaperId<=0)
            {
                return Content("试卷必须选择");
            }
            if (string.IsNullOrEmpty(model.PrizeName))
            {
                return Content("奖品名称不能为空");
            }
            if(model.PrizeImgUrl==null)
            {
                return Content("奖品图片不能为空");
            }
            long id= activityService.AddNew(model.Name, model.Description,model.StatusId, PicSave(model.imgUrl), model.StartTime, model.ExamEndTime, model.RewardTime, model.PaperId, model.PrizeName, PicSave(model.PrizeImgUrl));
            if(id<=0)
            {
                return Content("添加失败");
            }
            return Redirect("~/activity/list");
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
        [Permission("manager")]
        [HttpPost]
        public ActionResult PicUpload(long urlId, HttpPostedFileBase file)
        {    
            string md5 = CommonHelper.GetMD5(file.InputStream);
            string ext = Path.GetExtension(file.FileName);
            string path = "/upload/" + DateTime.Now.ToString("yyyy/MM/dd") + "/" + md5 + ext;
            //string thumbPath = "/upload/" + DateTime.Now.ToString("yyyy/MM/dd") + "/" + md5 + "_thumb" + ext;
            string fullPath = HttpContext.Server.MapPath("~" + path);
            //string thumbFullPath = HttpContext.Server.MapPath("~" + thumbPath);
            new FileInfo(fullPath).Directory.Create();
            //file.SaveAs(fullPath);
            //缩略图
            //file.InputStream.Position = 0;
            //ImageProcessingJob jobThumb = new ImageProcessingJob();
            //jobThumb.Filters.Add(new FixedResizeConstraint(200, 200));//缩略图尺寸 200*200
            //jobThumb.SaveProcessedImageToFileSystem(file.InputStream, thumbFullPath);
            //水印
            //file.InputStream.Position = 0;
            //ImageWatermark imgWatermark = new ImageWatermark(HttpContext.Server.MapPath("~/images/fb.png"));
            //imgWatermark.ContentAlignment = System.Drawing.ContentAlignment.BottomRight;//水印位置
            //imgWatermark.Alpha = 50;//透明度，需要水印图片是背景透明的 png 图片
            ImageProcessingJob jobNormal = new ImageProcessingJob();
            //jobNormal.Filters.Add(imgWatermark);//添加水印
            jobNormal.Filters.Add(new FixedResizeConstraint(600, 600));//限制图片的大小，避免生成
            jobNormal.SaveProcessedImageToFileSystem(file.InputStream, fullPath);

            return Json(new AjaxResult { Status ="success",Data=path});
        }

        public string PicSave(HttpPostedFileBase file)
        {
            string md5 = CommonHelper.GetMD5(file.InputStream);
            string ext = Path.GetExtension(file.FileName);
            string path = "/upload/" + DateTime.Now.ToString("yyyy/MM/dd") + "/" + md5 + ext;
            string fullPath = HttpContext.Server.MapPath("~" + path);
            new FileInfo(fullPath).Directory.Create();
            ImageProcessingJob jobNormal = new ImageProcessingJob();
            jobNormal.Filters.Add(new FixedResizeConstraint(600, 600));//限制图片的大小，避免生成
            jobNormal.SaveProcessedImageToFileSystem(file.InputStream, fullPath);
            return path;
        }
    }
}