using Chat.DTO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat.AdminWeb.Models
{
    public class LoadAddExeModel
    {
        public TestPaperDTO TestPaper { get; set; }
        public ExercisesDTO[] Exercises { get; set; }
        public long PaperExeCount { get; set; }
    }
}