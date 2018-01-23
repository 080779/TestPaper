using Chat.IService.Interface;
using Chat.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.DTO.DTO;
using System.Data.Entity;
using System.Data.SqlClient;
using Chat.WebCommon;

namespace Chat.Service.Service
{
    public class TestPaperService : ITestPaperService
    {
        public long AddNew(string testTitle, long exercisesCount)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                TestPaperEntity entity = new TestPaperEntity();
                entity.TestTitle = testTitle;
                entity.ExercisesCount = 0;
                dbc.TestPapers.Add(entity);
                dbc.SaveChanges();
                dbc.Database.ExecuteSqlCommand("update T_TestPapers set Num=(select CONVERT(varchar(12) , (select createdatetime from T_TestPapers where id=@id), 112)) where id=@id", new SqlParameter("@id", entity.Id));
                return entity.Id;
            }
        }

        public bool Delete(long id)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<TestPaperEntity> cs = new CommonService<TestPaperEntity>(dbc);
                var exe = cs.GetAll().SingleOrDefault(e => e.Id == id);
                if(exe==null)
                {
                    return false;
                }
                cs.MarkDeleted(id);
                return true;
            }
        }

        public TestPaperDTO[] GetAll()
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<TestPaperEntity> cs = new CommonService<TestPaperEntity>(dbc);
                
                return cs.GetAll().OrderByDescending(t=>t.CreateDateTime).Take(20).Select(r => new TestPaperDTO { Id = r.Id, TestTitle = r.TestTitle, ExercisesCount = r.ExercisesCount, CreateDateTime = r.CreateDateTime, Num = r.Num }).ToArray();
            }
        }

        public TestPaperDTO GetByActivityId(long id)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<TestPaperEntity> cs = new CommonService<TestPaperEntity>(dbc);
                CommonService<ActivityEntity> acs = new CommonService<ActivityEntity>(dbc);
                var activity= acs.GetAll().SingleOrDefault(a => a.Id == id);
                if(activity==null)
                {
                    return null;
                }
                var paper = cs.GetAll().SingleOrDefault(p => p.Id == activity.PaperId);
                if(paper==null)
                {
                    return null;
                }                
                return new TestPaperDTO { Id = paper.Id, TestTitle = paper.TestTitle, ExercisesCount = paper.ExercisesCount, CreateDateTime = paper.CreateDateTime, Num = paper.Num };
            }
        }

        public TestPaperDTO[] GetAllExcludeBelongActId(long id)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<TestPaperEntity> cs = new CommonService<TestPaperEntity>(dbc);
                CommonService<ActivityEntity> acs = new CommonService<ActivityEntity>(dbc);
                var activity = acs.GetAll().SingleOrDefault(a => a.Id == id);
                if (activity == null)
                {
                    return null;
                }
                var paper = cs.GetAll().Where(p => p.Id != activity.PaperId);
                if (paper == null)
                {
                    return null;
                }
                return paper.OrderByDescending(u=>u.CreateDateTime).Take(19).ToList().Select(u => new TestPaperDTO { Id = u.Id, TestTitle = u.TestTitle, ExercisesCount = u.ExercisesCount, CreateDateTime = u.CreateDateTime, Num = u.Num }).ToArray();
            }
        }

        public TestPaperDTO GetById(long paperId)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<TestPaperEntity> cs = new CommonService<TestPaperEntity>(dbc);
                var paper = cs.GetAll().SingleOrDefault(p => p.Id == paperId);
                if(paper==null)
                {
                    return null;
                }
                return new TestPaperDTO { Id = paper.Id, TestTitle = paper.TestTitle, ExercisesCount = paper.ExercisesCount, CreateDateTime = paper.CreateDateTime, Num = paper.Num };
            }
        }

        public TestPaperDTO[] Search(DateTime? startTime, DateTime? endTime, string keyWord)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<TestPaperEntity> cs = new CommonService<Entities.TestPaperEntity>(dbc);
                var items = cs.GetAll();
                if(startTime!= null)
                {
                    startTime = (DateTime)startTime;
                    items = items.Where(p => p.CreateDateTime >= startTime);
                }
                if(endTime!= null)
                {
                    endTime = (DateTime)endTime;
                    items = items.Where(p => p.CreateDateTime <= endTime);
                }
                if(!string.IsNullOrEmpty(keyWord))
                {
                    items = items.Where(p => p.TestTitle.Contains(keyWord));
                }
                items = items.OrderByDescending(p => p.CreateDateTime).Take(20);
                return items.ToList().Select(p => new TestPaperDTO { Id = p.Id, TestTitle = p.TestTitle, ExercisesCount = p.ExercisesCount, CreateDateTime = p.CreateDateTime ,Num=p.Num}).ToArray();
            }
        }

        public bool Update(long id,string title)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<TestPaperEntity> cs = new CommonService<TestPaperEntity>(dbc);
                var paper= cs.GetAll().SingleOrDefault(t=>t.Id==id);
                if(paper==null)
                {
                    return false;
                }
                paper.TestTitle = title;
                dbc.SaveChanges();
                return true;
            }
        }
    }
}
