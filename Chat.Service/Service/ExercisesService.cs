using Chat.IService.Interface;
using Chat.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.DTO.DTO;

namespace Chat.Service.Service
{
    public class ExercisesService : IExercisesService
    {
        public long AddNew(string title, long testPaperId, string optionA, string optionB, string optionC, string optionD, long rightKeyId)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<ExercisesEntity> cs = new CommonService<ExercisesEntity>(dbc);
                if(cs.GetAll().Any(e => e.Title == title))
                {
                    return -1;
                }
                ExercisesEntity exercises = new ExercisesEntity();
                exercises.Title = title;
                exercises.TestPaperId = testPaperId;
                exercises.OptionA = optionA;
                exercises.OptionB = optionB;
                exercises.OptionC = optionC;
                exercises.OptionD = optionD;
                exercises.RightKeyId = rightKeyId;
                exercises.Point = 0;

                switch(rightKeyId)
                {
                    case 1:
                        exercises.Tip = title + optionA;
                        break;
                    case 2:
                        exercises.Tip = title + optionB;
                        break;
                    case 3:
                        exercises.Tip = title + optionC;
                        break;
                    case 4:
                        exercises.Tip = title + optionD;
                        break;
                }
                dbc.Exercises.Add(exercises);
                dbc.SaveChanges();
                string sql = "update T_TestPapers set ExercisesCount=c.b from T_TestPapers, (select t.Id, COUNT(e.TestPaperId) as b from T_TestPapers t inner join (select TestPaperId from T_Exercises where IsDeleted=0) as e on t.Id=e.TestPaperId group by t.id) as c where T_TestPapers.Id=c.Id";
                dbc.Database.ExecuteSqlCommand(sql);
                return exercises.Id;
            }
        }

        public ExercisesDTO ToDTO(ExercisesEntity entity)
        {
            ExercisesDTO dto = new ExercisesDTO();
            dto.CreateDateTime = entity.CreateDateTime;
            dto.Id = entity.Id;
            dto.OptionA = entity.OptionA;
            dto.OptionB = entity.OptionB;
            dto.OptionC = entity.OptionC;
            dto.OptionD = entity.OptionD;
            dto.RightKeyId = entity.RightKeyId;
            dto.RightKeyName = entity.RightKey.Name;
            dto.TestPaperId = entity.TestPaperId;
            dto.TestPaperTitle = entity.TestPaper.TestTitle;
            dto.Title = entity.Title;
            dto.Tip = entity.Tip;     
            return dto;
        }

        /// <summary>
        /// 根据试卷id获得试题
        /// </summary>
        /// <param name="testPaperId"></param>
        /// <returns></returns>
        public ExercisesDTO[] GetExercisesByPaperId(long testPaperId)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<TestPaperEntity> csPaper = new CommonService<TestPaperEntity>(dbc);
                var paper = csPaper.GetAll().SingleOrDefault(p => p.Id == testPaperId);
                if (paper==null)
                {
                    return null;
                }
                CommonService<ExercisesEntity> cs = new CommonService<ExercisesEntity>(dbc);
                return cs.GetAll().Include(e => e.TestPaper).Include(e=>e.RightKey).Where(e=>e.TestPaperId==testPaperId).ToList().Select(e=>ToDTO(e)).ToArray();
            }
        }

        public long GetPaperExercisesCount(long testPaperId)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<ExercisesEntity> cs = new CommonService<ExercisesEntity>(dbc);
                return cs.GetAll().Include(e => e.TestPaper).LongCount(e => e.TestPaperId == testPaperId);
            }
        }

        public ExercisesDTO GetById(long id)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<ExercisesEntity> cs = new CommonService<ExercisesEntity>(dbc);
                var exe = cs.GetAll().Include(e => e.TestPaper).Include(e => e.RightKey).SingleOrDefault(e => e.Id == id);
                if(exe==null)
                {
                    return null;
                }
                return ToDTO(exe);
            }
        }

        public bool DelExercisesById(long id)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<ExercisesEntity> cs = new CommonService<ExercisesEntity>(dbc);
                var exe = cs.GetAll().Include(e => e.TestPaper).Include(e => e.RightKey).SingleOrDefault(e => e.Id == id);
                if (exe == null)
                {
                    return false; 
                }
                cs.MarkDeleted(id);
                string sql = "update T_TestPapers set ExercisesCount=c.b from T_TestPapers, (select t.Id, COUNT(e.TestPaperId) as b from T_TestPapers t inner join (select TestPaperId from T_Exercises where IsDeleted=0) as e on t.Id=e.TestPaperId group by t.id) as c where T_TestPapers.Id=c.Id";
                dbc.Database.ExecuteSqlCommand(sql);
                return true;
            }
        }

        public bool Update(long id, string title, string optionA, string optionB, string optionC, string optionD, long rightKeyId)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<ExercisesEntity> cs = new CommonService<ExercisesEntity>(dbc);
                var exe= cs.GetAll().Include(e => e.TestPaper).Include(e => e.RightKey).SingleOrDefault(e => e.Id == id);
                if(exe==null)
                {
                    return false;
                }
                exe.OptionA = optionA;
                exe.OptionB = optionB;
                exe.OptionC = optionC;
                exe.OptionD = optionD;
                exe.RightKeyId = rightKeyId;

                switch (rightKeyId)
                {
                    case 1:
                        exe.Tip = title + optionA;
                        break;
                    case 2:
                        exe.Tip = title + optionB;
                        break;
                    case 3:
                        exe.Tip = title + optionC;
                        break;
                    case 4:
                        exe.Tip = title + optionD;
                        break;
                }
                exe.Title = title;
                dbc.SaveChanges();
                return true;
            }
        }

        public bool IsRightOrWrong(long paperId,long id,long rightKeyId)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<ExercisesEntity> cs = new CommonService<ExercisesEntity>(dbc);
                var entity= cs.GetAll().Include(e => e.TestPaper).SingleOrDefault(e => e.TestPaperId == paperId && e.Id==id);
                if(entity==null)
                {
                    return false;
                }
                return entity.RightKeyId == rightKeyId;
            }
        }
    }
}
