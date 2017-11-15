using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Service.Entities
{
    /// <summary>
    /// 数据字典实体类
    /// </summary>
    public class IdNameEntity:BaseEntity
    {
        public string TypeName { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
    }
}
