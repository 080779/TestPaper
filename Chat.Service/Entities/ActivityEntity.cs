using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Service.Entities
{
    /// <summary>
    /// 活动实体类
    /// </summary>
    public class ActivityEntity:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public long StatusId { get; set; }
        public string WeChatUrl { get; set; }
        public virtual IdNameEntity Status { get; set; }
        public long PaperId { get; set; }
        public virtual TestPaperEntity Papers { get; set; }
        public string PrizeName { get; set; }
        public string PrizeImgUrl { get; set; }
        public virtual ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
        /// <summary>
        /// 访问数
        /// </summary>
        public long VisitCount { get; set; }
        /// <summary>
        /// 转发次数
        /// </summary>
        public long ForwardCount { get; set; }
        /// <summary>
        /// 答题人数
        /// </summary>
        public long AnswerCount { get; set; }
        /// <summary>
        /// 获奖资格人数
        /// </summary>
        public long HavePrizeCount { get; set; }
        /// <summary>
        /// 获奖人数
        /// </summary>
        public long PrizeCount { get; set; }
        /// <summary>
        /// 答题开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 答题截止时间
        /// </summary>
        public DateTime ExamEndTime { get; set; }
        /// <summary>
        /// 开奖时间
        /// </summary>
        public DateTime RewardTime { get; set; }
    }
}
