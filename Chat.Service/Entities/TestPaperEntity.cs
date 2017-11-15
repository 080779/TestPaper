using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Service.Entities
{
    /// <summary>
    /// 试卷实体类
    /// </summary>
    public class TestPaperEntity:BaseEntity
    {
        public string TestTitle { get; set; }
        public long ExercisesCount { get; set; }
    }
}
