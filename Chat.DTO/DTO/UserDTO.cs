using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.DTO.DTO
{
    public class UserDTO:BaseDTO
    {
        public string Name { get; set; }
        public string NickName { get; set; }
        public string PhotoUrl { get; set; }
        public string Mobile { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public int LoginErrorTimes { get; set; }
        public DateTime? LastLoginErrorDateTime { get; set; }
    }
}
