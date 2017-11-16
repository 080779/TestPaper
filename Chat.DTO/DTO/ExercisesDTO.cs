using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.DTO.DTO
{
    public class ExercisesDTO:BaseDTO
    {
        public string Title { get; set; }
        public long TestPaperId { get; set; }
        public string TestPaperTitle { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public long RightKeyId { get; set; }
        public string RightKeyName { get; set; }
        public int Point { get; set; }
        public string Tip { get; set; }
    }
}
