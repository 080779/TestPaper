using Chat.DTO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat.FrontWeb.Models
{
    public class PrizeViewModel
    {
        public string ActivityName { get; set; }
        public string PrizeName { get; set; }
        public string PrizeImgUrl { get; set; }
        UserDTO User { get; set; }
    }
}