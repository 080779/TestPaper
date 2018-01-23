using Chat.DTO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat.AdminWeb.Models
{
    public class PrizeSetModel
    {
        public UserDTO[] Users { get; set; }
        public long ActivityId { get; set; }
        public string Page { get; set; }
    }
}