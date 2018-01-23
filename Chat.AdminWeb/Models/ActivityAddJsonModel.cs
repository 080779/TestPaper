using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat.AdminWeb.Models
{
    public class ActivityAddJsonModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string imgUrl { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? ExamEndTime { get; set; }
        public DateTime? RewardTime { get; set; }
        public long PaperId { get; set; }
        public string PrizeName { get; set; }
        public string PrizeImgUrl { get; set; }
    }
}