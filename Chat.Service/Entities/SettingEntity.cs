using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Service.Entities
{
    /// <summary>
    /// 系统配置实体类
    /// </summary>
    public class SettingEntity:BaseEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
