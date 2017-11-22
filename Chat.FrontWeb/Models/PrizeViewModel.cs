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
        public int winCount { get; set; }
        public List<IsWonUser> Users { get; set; }
        public bool UserIsWon { get; set; }
        public DateTime PrizeTime { get; set; }
        public string PrizeFirstUrl { get; set; }
        public string StatusName { get; set; }
    }
}