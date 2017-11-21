using Chat.AdminWeb.App_Start;
using Chat.AdminWeb.Models;
using Chat.DTO.DTO;
using Chat.IService.Interface;
using Chat.WebCommon;
using CodeCarvings.Piczard;
using CodeCarvings.Piczard.Filters.Watermarks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
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
        public IUserService userService { get; set; }

        [Permission("list")]
        public ActionResult List(int pageIndex=1)
        {
            ActivityListModel model = new ActivityListModel();
            model.Activities = activityService.GetPageData(10, (pageIndex - 1) * 2);
            ViewBag.PageIndex = pageIndex;
            ViewBag.TotalCount =(int)activityService.GetTotalCount();
            return View(model);
        }

        [Permission("list")]
        public ActionResult UserActList(long id)
        {
            ActivityDTO[] dtos = activityService.GetByUserId(id);
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
        /// <summary>
        /// 添加活动
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Permission("manager")]
        public ActionResult Add(AtivityAddModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return Content(MVCHelper.GetValidMsg(ModelState));
            //}
            if (string.IsNullOrEmpty(model.Name))
            {
                return Content("活动名不能为空");
            }
            if (string.IsNullOrEmpty(model.Description))
            {
                return Content("活动简介不能为空");
            }
            if (model.StatusId <= 0)
            {
                return Content("活动状态必须选择");
            }
            //statusId=6为活动正在进行中
            if(activityService.CheckByStatusId(6))
            {
                return Content("有活动已经在进行中，不能存在两个同时进行的活动，请选择其他状态");
            }
            //if (model.imgUrl == null)
            //{
            //    return Content("活动背景图不能为空");
            //}
            string ext = Path.GetExtension(model.imgUrl.FileName);
            string[] imgs = { ".png", ".jpg", ".jpeg", ".bmp" };
            if (!imgs.Contains(ext))
            {
                return Content("请上传背景图片文件，支持格式“png、jpg、jpeg、bmp”");
            }
            if (model.StartTime == Convert.ToDateTime("0001-1-1 0:00:00"))
            {
                return Content("活动开始时间不能为空");
            }
            if (model.ExamEndTime == Convert.ToDateTime("0001-1-1 0:00:00"))
            {
                return Content("答题截止时间不能为空");
            }
            if (model.RewardTime == Convert.ToDateTime("0001-1-1 0:00:00"))
            {
                return Content("开奖时间不能为空");
            }
            if (model.PaperId <= 0)
            {
                return Content("试卷必须选择");
            }
            if (string.IsNullOrEmpty(model.PrizeName))
            {
                return Content("奖品名称不能为空");
            }
            //if (model.PrizeImgUrl == null)
            //{
            //    return Content("奖品图片不能为空");
            //}
            ext = Path.GetExtension(model.PrizeImgUrl.FileName);
            if (!imgs.Contains(ext))
            {
                return Content("请上传奖品图片文件，支持格式“png、jpg、jpeg、bmp”");
            }            
            long id= activityService.AddNew(model.Name, model.Description,model.StatusId, PicSave(model.imgUrl), model.StartTime, model.ExamEndTime, model.RewardTime, model.PaperId, model.PrizeName, PicSave(model.PrizeImgUrl));
            if(id<=0)
            {
                return Content("添加失败");
            }
            return Redirect("~/activity/list");
        }
        [Permission("manager")]
        public ActionResult Edit(long id)
        {
            ActivityDTO activity = activityService.GetById(id);
            if (activity==null)
            {
                return Content("数据不存在！");
            }
            ActivityEditLoadModel model = new ActivityEditLoadModel();
            model.Activity = activity;
            model.Paper = paperService.GetByActivityId(id);
            model.Status = idNameService.GetAll("活动状态");
            return View(model);
        }
        [HttpPost]
        [Permission("manager")]
        public ActionResult Edit(AtivityEditModel model)
        {
            string sImgPath = string.Empty;
            string sPrizeImgPath = string.Empty;

            if (model.activityId<=0)
            {
                return Content("该活动数据不存在");
            }
            if (string.IsNullOrEmpty(model.Name))
            {
                return Content("活动名不能为空");
            }
            if (string.IsNullOrEmpty(model.Description))
            {
                return Content("活动简介不能为空");
            }
            if (model.StatusId <= 0)
            {
                return Content("活动状态必须选择");
            }
            //判断此活动是否正在进行中，如果是就可以随便编辑状态，statusId=6为活动正在进行中
            if (!activityService.CheckByStatusId(model.activityId,6))
            {
                //当当前活动状态不为“进行中”，判断所有活动中是否有已经为“进行中的活动”，如果有提示
                if (model.StatusId==6)
                {
                    return Content("有活动已经在进行中，不能存在两个同时进行的活动，请选择其他状态");
                }
            }
            //if (model.imgUrl == null)
            //{
            //    return Content("活动背景图不能为空");
            //}
            string[] imgs = { ".png", ".jpg", ".jpeg", ".bmp" };
            if (model.imgUrl != null)
            {
                string ext = Path.GetExtension(model.imgUrl.FileName);
                
                if (!imgs.Contains(ext))
                {
                    return Content("请上传背景图片文件，支持格式“png、jpg、jpeg、bmp”");
                }
               
            }
             
            if (model.StartTime == Convert.ToDateTime("0001-1-1 0:00:00"))
            {
                return Content("活动开始时间不能为空");
            }
            if (model.ExamEndTime == Convert.ToDateTime("0001-1-1 0:00:00"))
            {
                return Content("答题截止时间不能为空");
            }
            if (model.RewardTime == Convert.ToDateTime("0001-1-1 0:00:00"))
            {
                return Content("开奖时间不能为空");
            }
            if (model.PaperId <= 0)
            {
                return Content("试卷必须选择");
            }
            if (string.IsNullOrEmpty(model.PrizeName))
            {
                return Content("奖品名称不能为空");
            }
            //if (model.PrizeImgUrl == null)
            //{
            //    return Content("奖品图片不能为空");
            //}
            if (model.PrizeImgUrl != null)
            {
                string ext = Path.GetExtension(model.PrizeImgUrl.FileName);
                if (!imgs.Contains(ext))
                {
                    return Content("请上传奖品图片文件，支持格式“png、jpg、jpeg、bmp”");
                }
                sPrizeImgPath = PicSave(model.PrizeImgUrl);
            }
            if (model.imgUrl != null)
                sImgPath = PicSave(model.imgUrl);
            
            bool b = activityService.Update( model.activityId,model.Name, model.Description, model.StatusId, sImgPath, model.StartTime, model.ExamEndTime, model.RewardTime, model.PaperId, model.PrizeName,sPrizeImgPath);
            if (!b)
            {
                return Content("编辑失败");
            }
            return Redirect("~/activity/list");
        }

        [Permission("manager")]
        public ActionResult DelActivity(long id)
        {
            if (!activityService.Delete(id))
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "答题活动删除失败" });
            }
            return Json(new AjaxResult { Status = "success" });
        }

        [Permission("manager")]
        public ActionResult Prize(long id)
        {
            PrizeSetModel model = new PrizeSetModel();
            model.Users = userService.GetByActivityIdHavePrize(id);
            model.ActivityId = id;
            return View(model);
        }
        [HttpPost]
        [Permission("manager")]
        public ActionResult PrizeSearch(long id, DateTime? startTime, DateTime? endTime, string keyWord)
        {
            if(id<=0)
            {
                return Json(new AjaxResult { Status="error",ErrorMsg="不存在这个答题活动"});
            }
            return Json(new AjaxResult { Status = "success", Data = userService.PrizeSearch(id,startTime, endTime, keyWord) });
        }

        [HttpPost]
        [Permission("manager")]
        public ActionResult PrizeWon(long[] isWonIds)
        {
            for(int i=0;i<isWonIds.Length;i++)
            {
                userService.SetWon(isWonIds[i]);
                userService.ReSetPrizeChance(isWonIds[i]);
            }
            return Json("success");
        }

        [Permission("manager")]
        public ActionResult CreateExcel(long id)
        {
            if(id<=0)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "不存在这个答题活动" });
            }
            UserDTO[] dtos = userService.GetByActivityIdIsWon(id);
            IWorkbook wb1 = new XSSFWorkbook();
            ISheet sheet1 = wb1.CreateSheet();
            sheet1.AutoSizeColumn(1);
            //sheet1.SetColumnWidth(0, 30 * 256);
            IRow Row1 = sheet1.CreateRow(0);
            ICellStyle style = wb1.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Center;
            //新建一个字体样式对象
            IFont font = wb1.CreateFont();
            //设置字体加粗样式
            font.Boldweight = short.MaxValue;
            //使用SetFont方法将字体样式添加到单元格样式中 
            style.SetFont(font);

            ICell cell0 = Row1.CreateCell(0);
            cell0.CellStyle = style;      
            cell0.SetCellValue("编号");

            ICell cell1 = Row1.CreateCell(1);
            cell1.CellStyle = style;
            cell1.SetCellValue("昵称");

            ICell cell2 = Row1.CreateCell(2);
            cell2.CellStyle = style;
            cell2.SetCellValue("姓名");

            ICell cell3 = Row1.CreateCell(3);
            cell3.CellStyle = style;
            cell3.SetCellValue("手机号");

            ICell cell4 = Row1.CreateCell(4);
            cell4.CellStyle = style;
            cell4.SetCellValue("联系地址");

            ICell cell5 = Row1.CreateCell(5);
            cell5.CellStyle = style;
            cell5.SetCellValue("参与活动次数");

            ICell cell6 = Row1.CreateCell(6);
            cell6.CellStyle = style;
            cell6.SetCellValue("中奖次数");

            int i = 1;
            foreach (var dto in dtos)
            {
                Row1 = sheet1.CreateRow(i++);
                cell0 = Row1.CreateCell(0);
                cell0.CellStyle = style;
                cell0.SetCellValue(dto.Id);
                cell1=Row1.CreateCell(1);
                cell1.CellStyle = style;
                cell1.SetCellValue(dto.NickName);
                cell2=Row1.CreateCell(2);
                cell2.CellStyle = style;
                cell2.SetCellValue(dto.Name);
                cell3=Row1.CreateCell(3);
                cell3.CellStyle = style;
                cell3.SetCellValue(dto.Mobile);
                cell4=Row1.CreateCell(4);
                cell4.CellStyle = style;
                cell4.SetCellValue(dto.Address);
                cell5=Row1.CreateCell(5);
                cell5.CellStyle = style;
                cell5.SetCellValue(dto.PassCount);
                cell6=Row1.CreateCell(6);
                cell6.CellStyle = style;
                cell6.SetCellValue(dto.WinCount);
            }

            for(int j=0;j<=6;j++)
            {
                sheet1.SetColumnWidth(j, 20*150);
            }
            sheet1.SetColumnWidth(4,40*256);

            var ms = new NpoiMemoryStream();
            ms.AllowClose = false;
            wb1.Write(ms);
            ms.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            ms.AllowClose = true;
            // 写入到客户端 
            return File(ms, "application/vnd.ms-excel", "获奖用户信息.xls");
        }

        [Permission("manager")]
        [HttpPost]
        public ActionResult PicUpload(long urlId, HttpPostedFileBase file)
        {    
            string md5 = CommonHelper.GetMD5(file.InputStream);
            string ext = Path.GetExtension(file.FileName);
            string path = "/upload/" + DateTime.Now.ToString("yyyy/MM/dd") + "/" + md5 + ext;
            //string thumbPath = "/upload/" + DateTime.Now.ToString("yyyy/MM/dd") + "/" + md5 + "_thumb" + ext;
            string fullPath = HttpContext.Server.MapPath("" + path);
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
            jobNormal.Filters.Add(new FixedResizeConstraint(512, 512));//限制图片的大小，避免生成
            jobNormal.SaveProcessedImageToFileSystem(file.InputStream, fullPath);
            return path;
        }

        public string BackImgSave(HttpPostedFileBase file)
        {
            string md5 = CommonHelper.GetMD5(file.InputStream);
            string ext = Path.GetExtension(file.FileName);
            string path = "/upload/" + DateTime.Now.ToString("yyyy/MM/dd") + "/" + md5 + ext;
            string fullPath = HttpContext.Server.MapPath("~" + path);
            new FileInfo(fullPath).Directory.Create();
            ImageProcessingJob jobNormal = new ImageProcessingJob();
            jobNormal.Filters.Add(new FixedResizeConstraint(360, 640));//限制图片的大小，避免生成
            jobNormal.SaveProcessedImageToFileSystem(file.InputStream, fullPath);
            return path;
        }

        public ActionResult Search(long? statusId,DateTime? startTime,DateTime? endTime,string keyWord)
        {
            ViewBag.PageIndex = 1;            
            ActivityDTO[] dtos = activityService.Search(statusId, startTime, endTime, keyWord);
            ViewBag.TotalCount = dtos.Count();
            return Json(new AjaxResult { Status = "success", Data = dtos });
        }

        public ActionResult SetData()
        {
            return Json("aa");
        }
    }
}