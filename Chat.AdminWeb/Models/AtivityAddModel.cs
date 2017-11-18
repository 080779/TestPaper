using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Chat.AdminWeb.Models
{
    public class AtivityAddModel
    {
        [Required(ErrorMessage ="活动名称不能为空")]
        public string Name { get; set; }
        [Required(ErrorMessage = "活动描述不能为空")]
        public string Description { get; set; }
        public long StatusId { get; set; }
        [Required]
        public HttpPostedFileBase imgUrl { get; set; }
        [Required(ErrorMessage = "活动开始时间不能为空")]
        public DateTime StartTime { get; set; }
        [Required(ErrorMessage = "答题截止时间不能为空")]
        public DateTime ExamEndTime { get; set; }
        [Required(ErrorMessage = "开奖时间不能为空")]
        public DateTime RewardTime { get; set; }
        public long PaperId { get; set; }
        [Required(ErrorMessage = "奖品名称不能为空")]
        public string PrizeName { get; set; }
        [Required]
        public HttpPostedFileBase PrizeImgUrl { get; set; }
    }
}