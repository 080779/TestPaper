using Chat.IService.Interface;
using Chat.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Service.Service
{
    public class ActivityImgService : IActivityImgService
    {
        public long AddNew(long activityId, string description, string imgUrl)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                ActivityImgEntity activityImg = new ActivityImgEntity();
                activityImg.ActivityId = activityId;
                activityImg.Description = description;
                activityImg.ImgUrl = imgUrl;
                dbc.ActivityImgs.Add(activityImg);
                dbc.SaveChanges();
                return activityImg.Id;
            }
        }
    }
}
