using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.DTO.DTO
{
    public class AdminUserDTO:BaseDTO
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public bool Gender { get; set; }
        public string Email { get; set; }
        //public long? CityId { get; set; }
        //public string CityName { get; set; }
        public int LoginErrorTimes { get; set; }
        public DateTime? LastLoginErrorDateTime { get; set; }
    }
}
