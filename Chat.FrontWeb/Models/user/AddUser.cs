using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Chat.FrontWeb.Models.user
{
    public class AddUser
    {
        public long Id { get; set; }
        [Required(ErrorMessage ="用户名必须填")]
        [StringLength(60,MinimumLength =2,ErrorMessage ="姓名要在2到30个字之间")]
        public string Name { get; set; }
        [Required(ErrorMessage = "手机号必须填")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "请输入11位手机号")]
        public string Mobile { get; set; }
        public bool Gender { get; set; }
        [Required(ErrorMessage = "地址必须填")]
        [StringLength(256, MinimumLength = 2, ErrorMessage = "地址要大于个字之间")]
        public string Address { get; set; }
    }
}