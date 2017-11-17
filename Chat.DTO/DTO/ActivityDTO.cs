using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.DTO.DTO
{
    public class ActivityDTO:BaseDTO
    {
        public string Num { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public long StatusId { get; set; }
        public string StatusName { get; set; }
        public long PaperId { get; set; }
        public string PaperTitle { get; set; }
        public string PrizeName { get; set; }
        public string PrizeImgUrl { get; set; }
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
