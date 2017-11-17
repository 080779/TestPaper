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

namespace Chat.Service.Service
{
    public class ActivityService : IActivityService
    {
        public long AddNew(string name, string description,long statusId, string imgUrl, DateTime startTime, DateTime examEndTime, DateTime rewardTime, long paperId, string prizeName, string prizeImgUrl)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                ActivityEntity activity = new ActivityEntity();
                activity.Name = name;
                activity.Description = description;
                activity.ImgUrl = imgUrl;
                activity.StartTime = startTime;
                activity.ExamEndTime = examEndTime;
                activity.RewardTime = rewardTime;
                activity.PaperId = paperId;
                activity.PrizeName = prizeName;
                activity.PrizeImgUrl = prizeImgUrl;
                activity.AnswerCount = 0;
                activity.ForwardCount = 0;
                activity.HavePrizeCount = 0;
                activity.PrizeCount = 0;
                activity.StatusId = statusId;
                activity.VisitCount = 0;
                dbc.Activities.Add(activity);
                dbc.SaveChanges();
                dbc.Database.ExecuteSqlCommand("update T_Activities set Num=(select CONVERT(varchar(12) , (select createdatetime from t_activities where id=@id), 112)) where id=@id",new SqlParameter("@id",activity.Id));
                return activity.Id;
            }
        }

        public ActivityDTO ToDTO(ActivityEntity entity)
        {
            ActivityDTO dto = new ActivityDTO();
            dto.AnswerCount = entity.AnswerCount;
            dto.CreateDateTime = entity.CreateDateTime;
            dto.Description = entity.Description;
            dto.ExamEndTime = entity.ExamEndTime;
            dto.ForwardCount = entity.ForwardCount;
            dto.HavePrizeCount = entity.HavePrizeCount;
            dto.Id = entity.Id;
            dto.Name = entity.Name;
            dto.PrizeCount = entity.PrizeCount;
            dto.RewardTime = entity.RewardTime;
            dto.StartTime = entity.StartTime;
            dto.StatusId = entity.StatusId;
            dto.StatusName = entity.Status.Name;
            dto.PaperId = entity.PaperId;
            dto.PaperTitle = entity.Papers.TestTitle;
            dto.PrizeName = entity.PrizeName;
            dto.ImgUrl = entity.ImgUrl;
            dto.PrizeImgUrl = entity.PrizeImgUrl;
            dto.VisitCount = entity.VisitCount;
            dto.WeChatUrl = entity.WeChatUrl;
            dto.Num = entity.Num;
            return dto;
        }

        public ActivityDTO[] GetAll()
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<ActivityEntity> cs = new CommonService<ActivityEntity>(dbc);
                return cs.GetAll().Include(a => a.Status).Include(a=>a.Papers).OrderByDescending(a => a.CreateDateTime).ToList().Select(a => ToDTO(a)).ToArray();
            }
        }

        public ActivityDTO GetNew()
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<ActivityEntity> cs = new CommonService<ActivityEntity>(dbc);
                ActivityEntity entity = cs.GetAll().OrderByDescending(a => a.CreateDateTime).First();
                return ToDTO(entity);
            }
        }

        public ActivityDTO GetById(long id)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<ActivityEntity> cs = new CommonService<ActivityEntity>(dbc);
                ActivityEntity entity = cs.GetAll().Include(a => a.Status).Include(a => a.Papers).SingleOrDefault(a => a.Id == id);
                if(entity==null)
                {
                    return null;
                }
                return ToDTO(entity);
            }
        }

        public ActivityDTO GetByStatus(string statusName)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<ActivityEntity> cs = new CommonService<ActivityEntity>(dbc);
                ActivityEntity entity = cs.GetAll().Include(a => a.Status).Include(a => a.Papers).SingleOrDefault(a => a.Status.Name == statusName);
                if (entity == null)
                {
                    return null;
                }
                return ToDTO(entity);
            }
        }

        public bool Update(long id, string name, string description, long statusId, string imgUrl, DateTime startTime, DateTime examEndTime, DateTime rewardTime, long paperId, string prizeName, string prizeImgUrl)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<ActivityEntity> cs = new CommonService<ActivityEntity>(dbc);
                var activity = cs.GetAll().Include(a => a.Status).Include(a => a.Papers).SingleOrDefault(a=>a.Id==id);
                if(activity == null)
                {
                    return false;
                }
                activity.Name = name;
                activity.Description = description;
                activity.ImgUrl = imgUrl;
                activity.StartTime = startTime;
                activity.ExamEndTime = examEndTime;
                activity.RewardTime = rewardTime;
                activity.PaperId = paperId;
                activity.PrizeName = prizeName;
                activity.PrizeImgUrl = prizeImgUrl;
                activity.AnswerCount = 0;
                activity.ForwardCount = 0;
                activity.HavePrizeCount = 0;
                activity.PrizeCount = 0;
                activity.StatusId = statusId;
                activity.VisitCount = 0;
                dbc.SaveChanges();
                return true;
            }
        }

        public ActivityDTO[] Search(long? statusId, DateTime? startTime, DateTime? endTime, string keyWord)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<ActivityEntity> cs = new CommonService<Entities.ActivityEntity>(dbc);
                var items = cs.GetAll();
                if(statusId!=null)
                {
                    items = items.Where(p => p.StatusId == statusId);
                }
                if (startTime != null)
                {
                    items = items.Where(p => p.CreateDateTime >= startTime);
                }
                if (endTime != null)
                {
                    items = items.Where(p => p.CreateDateTime <= endTime);
                }
                if (!string.IsNullOrEmpty(keyWord))
                {
                    items = items.Where(p => p.Name.Contains(keyWord) || p.Description.Contains(keyWord));
                }
                items = items.OrderByDescending(p => p.CreateDateTime).Take(10);
                return items.ToList().Select(p => ToDTO(p)).ToArray();
            }
        }
    }
}
