using Chat.IService.Interface;
using Chat.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.DTO.DTO;
using System.Data.Entity;

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
                
                return cs.GetAll().OrderByDescending(t=>t.CreateDateTime).Select(r => new TestPaperDTO { Id = r.Id, TestTitle = r.TestTitle, ExercisesCount = r.ExercisesCount, CreateDateTime = r.CreateDateTime }).ToArray();
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
                return new TestPaperDTO { Id = paper.Id, TestTitle = paper.TestTitle, ExercisesCount = paper.ExercisesCount, CreateDateTime = paper.CreateDateTime };
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
