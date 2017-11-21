using Chat.DTO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat.FrontWeb.Models
{
    public class ActivityViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StartTime { get; set; }
        public string ExamEndTime { get; set; }
        public string RewardTime { get; set; }
        public long AnswerCount { get; set; }
        public string StatusName { get; set; }
    }
}