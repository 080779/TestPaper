using Chat.DTO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat.AdminWeb.Models
{
    public class ActivityAddLoadModel
    {
        public TestPaperDTO[] Papers { get; set; }
        public IdNameDTO[] Status { get; set; }
    }
}