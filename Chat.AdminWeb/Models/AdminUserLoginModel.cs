using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Chat.AdminWeb.Models
{
    public class AdminUserLoginModel
    {
        [Required]
        [StringLength(20,MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        [StringLength(120, MinimumLength = 6)]
        public string Password { get; set; }
    }
}