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
                return cs.GetAll().Include(a => a.Status).Include(a=>a.Papers).OrderByDescending(a => a.CreateDateTime).Take(10).ToList().Select(a => ToDTO(a)).ToArray();
            }
        }

        public ActivityDTO GetNew()
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<ActivityEntity> cs = new CommonService<ActivityEntity>(dbc);
                long count = cs.GetTotalCount();
                if(count<=0)
                {
                    return null;
                }
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

        /// <summary>
        /// 判断活动id 的活动是否存在
        /// </summary>
        /// <param name="id">活动id</param>
        /// <returns></returns>
        public bool ExistActivity(long id)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<ActivityEntity> cs = new CommonService<ActivityEntity>(dbc);
                return cs.GetAll().Any(a=>a.Id==id);
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
                if(!string.IsNullOrWhiteSpace(imgUrl))
                    activity.ImgUrl = imgUrl;
                activity.StartTime = startTime;
                activity.ExamEndTime = examEndTime;
                activity.RewardTime = rewardTime;
                activity.PaperId = paperId;
                activity.PrizeName = prizeName;
                if (!string.IsNullOrWhiteSpace(prizeImgUrl))
                    activity.PrizeImgUrl = prizeImgUrl;
                activity.StatusId = statusId;
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
                    startTime = DateTimeHelper.GetBeginDate((DateTime)startTime);
                    items = items.Where(p => p.CreateDateTime >= startTime);
                }
                if (endTime != null)
                {
                    endTime = DateTimeHelper.GetEndDate((DateTime)endTime);
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

        /// <summary>
        /// 根据条件查询活动
        /// </summary>
        /// <param name="statusId">活动状态</param>
        /// <param name="startTime">活动开始时间</param>
        /// <param name="endTime">活动结束时间</param>
        /// <param name="keyWord">关键字</param>
        /// <param name="currentIndex">跳过的条数（（当前页-1）*每页数）</param>
        /// <param name="pageSize">每页数</param>
        /// <returns></returns>
        public ActivitySearchResult Search(long? statusId, DateTime? startTime, DateTime? endTime, string keyWord, int currentIndex, int pageSize)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<ActivityEntity> cs = new CommonService<Entities.ActivityEntity>(dbc);
                var items = cs.GetAll();
                if (statusId != null)
                {

                    items = items.Where(p => p.StatusId == statusId);
                }
                if (startTime != null)
                {
                    startTime = DateTimeHelper.GetBeginDate((DateTime)startTime);
                    items = items.Where(p => p.CreateDateTime >= startTime);
                }
                if (endTime != null)
                {
                    endTime = DateTimeHelper.GetEndDate((DateTime)endTime);
                    items = items.Where(p => p.CreateDateTime <= endTime);
                }
                if (!string.IsNullOrEmpty(keyWord))
                {
                    items = items.Where(p => p.Name.Contains(keyWord) || p.Description.Contains(keyWord));
                }
                items = items.OrderByDescending(p => p.CreateDateTime);
                int count = items.Count();
                ActivitySearchResult result = new ActivitySearchResult();
                result.Activities= items.Skip(currentIndex).Take(pageSize).ToList().Select(p => ToDTO(p)).ToArray();
                result.TotalCount = count;
                return result;
            }
        }

        public class ActivitySearchResult
        {
            public ActivityDTO[] Activities { get; set; }
            public int TotalCount { get; set; }
        }

        public bool Delete(long id)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<ActivityEntity> cs = new CommonService<ActivityEntity>(dbc);
                var activity= cs.GetAll().SingleOrDefault(a=>a.Id==id);
                if(activity==null)
                {
                    return false;
                }
                cs.MarkDeleted(id);
                return true;
            }
        }

        public bool AddUserId(long activityId, long userId)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<ActivityEntity> acs = new CommonService<ActivityEntity>(dbc);
                CommonService<UserEntity> ucs = new CommonService<UserEntity>(dbc);
                var act = acs.GetAll().Include(a=>a.Users).SingleOrDefault(a => a.Id == activityId);
                var user = ucs.GetAll().SingleOrDefault(u => u.Id == userId);
                if(act==null)
                {
                    return false;
                }
                if(user==null)
                {
                    return false;
                }
                var count = dbc.Database.SqlQuery<long>("select UserId from t_useractivities where ActivityId=@activityId and UserId=@userId",new SqlParameter("@activityId", activityId), new SqlParameter("@userId", userId));
                if (count.Count()>=1)
                {
                    return false;
                }      
                act.Users.Add(user);
                dbc.SaveChanges();
                user.PassCount++;
                dbc.SaveChanges();
                return true;
            }
        }

        public ActivityDTO[] GetByUserId(long id)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<UserEntity> ucs = new CommonService<UserEntity>(dbc);
                var user = ucs.GetAll().SingleOrDefault(u=>u.Id==id);
                if(user==null)
                {
                    return null;
                }
                return dbc.Database.SqlQuery<ActivityDTO>("select top(10) a.ID,a.Num,a.Name,a.Description,a.ImgUrl,a.StatusId,i.Name as StatusName,a.PaperId,t.TestTitle as PaperTitle,a.PrizeName,a.PrizeImgUrl,a.WeChatUrl,a.VisitCount,a.ForwardCount,a.AnswerCount,a.HavePrizeCount,a.PrizeCount,a.StartTime,a.ExamEndTime,a.RewardTime from T_Activities as a left join t_idnames i on i.id=a.statusid left join T_TestPapers t on t.Id=a.PaperId, (select ActivityId from T_UserActivities where UserId=@id) as u where a.Id=u.ActivityId and a.IsDeleted=0", new SqlParameter("@id",id)).ToArray();
            }
        }

        public ActivityDTO[] GetActivityByUserId(long id)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<UserEntity> ucs = new CommonService<UserEntity>(dbc);
                CommonService<ActivityEntity> cs = new CommonService<ActivityEntity>(dbc);
                var user = ucs.GetAll().SingleOrDefault(u => u.Id == id);
                if (user == null)
                {
                    return null;
                }
                //linq查询语句多对多查询
                var act = from a in dbc.Activities
                          from u in a.Users
                          where u.Id == id
                          select a;
               return act.OrderByDescending(a=>a.CreateDateTime).Take(10).ToList().Select(a => ToDTO(a)).ToArray();
            }
        }

        public ActivityDTO[] GetPageData(int pageSize, int currentIndex)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<ActivityEntity> cs = new CommonService<ActivityEntity>(dbc);
                return cs.GetAll().Include(a => a.Status).Include(a => a.Papers).OrderByDescending(a => a.CreateDateTime).Skip(currentIndex).Take(pageSize).ToList().Select(a => ToDTO(a)).ToArray();
            }
        }

        public long GetTotalCount()
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<ActivityEntity> cs = new CommonService<ActivityEntity>(dbc);
                return cs.GetTotalCount();
            }
        }

        public bool CheckByStatusId(long id,long statusId)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<ActivityEntity> cs = new CommonService<ActivityEntity>(dbc);
                return cs.GetAll().Any(a => a.Id==id && a.StatusId == statusId);
            }
        }

        public bool CheckByStatusId(long statusId)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<ActivityEntity> cs = new CommonService<ActivityEntity>(dbc);
                return cs.GetAll().Any(a => a.StatusId == statusId);
            }
        }

        public bool CheckByPaperId(long id)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<ActivityEntity> cs = new CommonService<ActivityEntity>(dbc);
                return cs.GetAll().Any(a => a.PaperId == id);
            }
        }

        /// <summary>
        /// 更新访问次数等
        /// </summary>
        /// <param name="id"></param>
        /// <param name="setVisitCount">访问次数</param>
        /// <param name="setForwardCount">转发次数</param>
        /// <param name="setAnswerCount">答题人数</param>
        /// <param name="setHavePrizeCount">获奖资格人数</param>
        /// <param name="setPrizeCount">获奖人数</param>
        /// <returns></returns>
        public bool UpdateCount(long id, bool setVisitCount,bool setForwardCount,bool setAnswerCount, bool setHavePrizeCount,bool setPrizeCount)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<ActivityEntity> cs = new CommonService<ActivityEntity>(dbc);
                ActivityEntity entity = cs.GetAll().SingleOrDefault(a=>a.Id==id);
                if(entity==null)
                {
                    return false;
                }
                if(setVisitCount)
                {
                    entity.VisitCount++;
                }
                if(setForwardCount)
                {
                    entity.ForwardCount++;
                }
                if (setAnswerCount)
                {
                    entity.AnswerCount++;
                }
                if (setHavePrizeCount)
                {
                    entity.HavePrizeCount++;
                }
                if(setPrizeCount)
                {
                    entity.PrizeCount++;
                }
                dbc.SaveChanges();
                return true;
            }
        }

        public bool CheckHaveStatusId(long id)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<ActivityEntity> cs = new CommonService<ActivityEntity>(dbc);
                return cs.GetAll().Any(a => a.StatusId == id);
            }
        }
    }
}
