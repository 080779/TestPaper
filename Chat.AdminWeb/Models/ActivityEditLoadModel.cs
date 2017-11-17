using Chat.DTO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat.AdminWeb.Models
{
    public class ActivityEditLoadModel
    {
        public IdNameDTO[] Status { get; set; }
        public ActivityDTO Activity { get; set; }
        public TestPaperDTO Paper { get; set; }
    }
}