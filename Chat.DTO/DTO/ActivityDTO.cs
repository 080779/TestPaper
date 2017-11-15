using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.DTO.DTO
{
    public class ActivityDTO:BaseDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long StatusId { get; set; }
        public string StatusName { get; set; }
        public string WeChatUrl { get; set; }
        public long VisitCount { get; set; }
        public long ForwardCount { get; set; }
        public long AnswerCount { get; set; }
        public long HavePrizeCount { get; set; }
        public long PrizeCount { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime ExamEndTime { get; set; }
        public DateTime RewardTime { get; set; }
    }
}
