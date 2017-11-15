using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Service.Entities
{
    /// <summary>
    /// 活动图片实体类
    /// </summary>
    public class ActivityImgEntity:BaseEntity
    {
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public long ActivityId { get; set; }
        public virtual ActivityEntity Activity { get; set; }
    }
}
