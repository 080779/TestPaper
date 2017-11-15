using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Service.Entities
{
    /// <summary>
    /// 角色实体类
    /// </summary>
    public class RoleEntity:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<AdminUserEntity> AdminUsers { get; set; } = new List<AdminUserEntity>();
        public virtual ICollection<PermissionEntity> Permissions { get; set; } = new List<PermissionEntity>();
    }
}
