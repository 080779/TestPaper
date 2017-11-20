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
        public IActivityService activityService { get; set; }

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
                if(!exercisesService.Update(model.ExeId, model.Title, model.OptionA, model.OptionB, model.OptionC, model.OptionD, model.RightKeyId,""))
                {
                    return Json(new AjaxResult { Status = "error", ErrorMsg = "考题编辑失败" });
                }
                return Json(new AjaxResult { Status = "ok"});
            }
            LoadAddExeModel loadmodel = new LoadAddExeModel();
            long exId= exercisesService.AddNew(model.Title, model.TestPaperId, model.OptionA, model.OptionB, model.OptionC, model.OptionD, model.RightKeyId,"");
            if(exId<=0)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "添加错误或考题已经存在" });
            }
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

        /// <summary>
        /// 添加试卷
        /// </summary>
        /// <param name="testTitle"></param>
        /// <returns></returns>
        [Permission("manager")]
        [HttpPost]
        public ActionResult AddPaper(string testTitle)
        {
            if(string.IsNullOrEmpty(testTitle))
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg="试卷标题不能为空" });
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

        /// <summary>
        /// 编辑试卷
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        [HttpPost]
        [Permission("manager")]
        public ActionResult EditPaper(long id,string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "试卷标题不能为空" });
            }
            if(!testPaperService.Update(id, title))
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "试卷编辑失败" });
            }
            return Json(new AjaxResult { Status = "success"});
        }
        /// <summary>
        /// 删除试卷
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Permission("manager")]
        public ActionResult DelPaper(long id)
        {
            if(activityService.CheckByPaperId(id))
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "有活动正在使用此试卷，无法删除" });
            }
            if(!testPaperService.Delete(id))
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "试卷删除失败" });
            }
            return Json(new AjaxResult { Status="success"});
        }
        [HttpPost]
        [Permission("manager")]
        public ActionResult Search(DateTime? startTime,DateTime? endTime,string keyWord)
        {
            return Json(new AjaxResult { Status="success",Data=testPaperService.Search(startTime,endTime,keyWord)});
        }
    }
}