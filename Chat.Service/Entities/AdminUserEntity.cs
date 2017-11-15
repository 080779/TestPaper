using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Service.Entities
{
    /// <summary>
    /// 后台用户实体类
    /// </summary>
    public class AdminUserEntity:BaseEntity
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public bool Gender { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        //public long? CityId { get; set; }
        //public virtual CityEntity City { get; set; }
        public int LoginErrorTimes { get; set; }
        public DateTime? LastLoginErrorTime { get; set; }
        public virtual ICollection<RoleEntity> Roles { get; set; } = new List<RoleEntity>();
    }
}
