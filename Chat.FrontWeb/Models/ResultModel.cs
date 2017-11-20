using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat.FrontWeb.Models
{
    public class ResultModel
    {
        public string ActivityName { get; set; }
        public long Id { get; set; }
        public List<string> Result { get; set; }
        public bool IsAllRight { get; set; }
    }
}