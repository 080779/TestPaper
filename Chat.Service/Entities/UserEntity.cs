using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Service.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; }
        public string NickName { get; set; }
        public string PhotoUrl { get; set; }
        public string Mobile { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; }
        public virtual ICollection<ActivityEntity> Activities { get; set; } = new List<ActivityEntity>();
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public int LoginErrorTimes { get; set; }
        public DateTime? LastLoginErrorDateTime { get; set; }
    }
}
