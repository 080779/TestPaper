using Chat.IService.Interface;
using Chat.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Service.Service
{
    public class ActivityService : IActivityService
    {
        public long AddNew(string name, string description, DateTime startTime, DateTime examEndTime, DateTime rewardTime)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<ActivityEntity> cs = new CommonService<ActivityEntity>(dbc);
                if(cs.GetAll().Any(a => a.Name == name))
                {
                    return -1;
                }
                ActivityEntity activity = new ActivityEntity();
                activity.Name = name;
                activity.Description = description;
                activity.StartTime = startTime;
                activity.ExamEndTime = examEndTime;
                activity.RewardTime = rewardTime;
                dbc.Activities.Add(activity);
                dbc.SaveChanges();
                return activity.Id;
            }
        }
    }
}
