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
    }
}
