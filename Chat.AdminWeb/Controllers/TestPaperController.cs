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
    public class TestPaperController : Controller
    {
        public ITestPaperService testPaperService { get; set; }
        public IExercisesService exercisesService { get; set; }

        [Permission("list")]
        public ActionResult List()
        {
            TestPaperDTO[] dtos = testPaperService.GetAll();
            return View(dtos);
        }

        [Permission("manager")]
        public ActionResult AddEditExe(long testPaperId)
        {
            LoadAddExeModel model = new Models.LoadAddExeModel();
            model.TestPaper= testPaperService.GetById(testPaperId);
            model.PaperExeCount = exercisesService.GetPaperExercisesCount(testPaperId);
            model.Exercises = exercisesService.GetExercisesByPaperId(testPaperId);
            return View(model);
        }

        /// <summary>
        /// 添加考题
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Permission("manager")]
        public ActionResult AddEditExe(AddExercisesModel model)
        {
            if (string.IsNullOrEmpty(model.Title))
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "考题题目不能为空" });
            }
            if (string.IsNullOrEmpty(model.OptionA) || string.IsNullOrEmpty(model.OptionB) || string.IsNullOrEmpty(model.OptionC) || string.IsNullOrEmpty(model.OptionD))
            {
                return Json(new AjaxResult { Status="error",ErrorMsg="选项内容不能为空"});
            }            
            if(model.RightKeyId<=0)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "请选择正确答案" });
            }
            if(model.ExeId>=1)
            {
                return Json(new AjaxResult { Status = "clear", ErrorMsg = "考题已经添加过，请点击清空内容添加" });
            }
            LoadAddExeModel loadmodel = new LoadAddExeModel();
            long exId= exercisesService.AddNew(model.Title, model.TestPaperId, model.OptionA, model.OptionB, model.OptionC, model.OptionD, model.RightKeyId);
            loadmodel.Exercises = exercisesService.GetExercisesByPaperId(model.TestPaperId);
            loadmodel.PaperExeCount = exercisesService.GetPaperExercisesCount(model.TestPaperId);
            loadmodel.TestPaper = testPaperService.GetById(model.TestPaperId);
            return Json(new AjaxResult { Status = "success", Data=loadmodel });
        }

        /// <summary>
        /// 加载考题
        /// </summary>
        /// <param name="exeId"></param>
        /// <returns></returns>
        [Permission("manager")]
        public ActionResult LoadExe(long exeId)
        {
            ExercisesDTO dto = exercisesService.GetById(exeId);
            if(dto==null)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg="考题不存在" });
            }
            return Json(new AjaxResult { Status = "success", Data = dto });
        }

        /// <summary>
        /// 删除考题
        /// </summary>
        /// <param name="paperId"></param>
        /// <param name="exeId"></param>
        /// <returns></returns>
        [HttpPost]
        [Permission("manager")]
        public ActionResult DelExe(long paperId,long exeId)
        {
            if (exeId <= 0)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "要删除的考题不存在" });
            }
            LoadAddExeModel loadmodel = new LoadAddExeModel();
            bool b = exercisesService.DelExercisesById(exeId);
            if(!b)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "删除失败" });
            }
            loadmodel.Exercises = exercisesService.GetExercisesByPaperId(paperId);
            loadmodel.PaperExeCount = exercisesService.GetPaperExercisesCount(paperId);
            loadmodel.TestPaper = testPaperService.GetById(paperId);
            return Json(new AjaxResult { Status = "success", Data = loadmodel });
        }

        [Permission("manager")]
        public ActionResult AddPaper()
        {
            return View();
        }
        [Permission("manager")]
        [HttpPost]
        public ActionResult AddPaper(string testTitle)
        {
            if(string.IsNullOrEmpty(testTitle))
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg="标题不能为空" });
            }
            long id = testPaperService.AddNew(testTitle,0);
            return Json(new AjaxResult { Status="success"});
        }
        [Permission("manager")]
        public ActionResult EditPaper(long id)
        {
            TestPaperDTO dto = testPaperService.GetById(id);
            return View(dto);
        }
        [HttpPost]
        [Permission("manager")]
        public ActionResult EditPaper(long id,string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "标题不能为空" });
            }
            if(!testPaperService.Update(id, title))
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "编辑失败" });
            }
            return Json(new AjaxResult { Status = "success"});
        }
    }
}