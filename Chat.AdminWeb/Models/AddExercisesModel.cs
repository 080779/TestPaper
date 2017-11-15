using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat.AdminWeb.Models
{
    public class AddExercisesModel
    {
        public string Title { get; set; }
        public long TestPaperId { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public long RightKeyId { get; set; }
        public long ExeId { get; set; }
    }
}