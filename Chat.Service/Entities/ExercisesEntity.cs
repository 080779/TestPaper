using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Service.Entities
{
    /// <summary>
    /// 试题实体类
    /// </summary>
    public class ExercisesEntity:BaseEntity
    {
        public string Title { get; set; }
        public long TestPaperId { get; set; }
        public virtual TestPaperEntity TestPaper { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public long RightKeyId { get; set; }
        public virtual IdNameEntity RightKey { get; set; }
        public int Point { get; set; }
    }
}
