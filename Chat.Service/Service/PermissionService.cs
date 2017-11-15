using Chat.IService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.DTO.DTO;
using Chat.Service.Entities;
using System.Data.Entity;

namespace Chat.Service.Service
{
    public class PermissionService : IPermissionService
    {
        public long AddNew(string name, string description)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<PermissionEntity> cs = new CommonService<PermissionEntity>(dbc);
                if(cs.GetAll().Any(p=>p.Name==name))
                {
                    return -1;
                }
                PermissionEntity permission = new PermissionEntity();
                permission.Name = name;
                permission.Description = description;
                dbc.Permissions.Add(permission);
                dbc.SaveChanges();
                return permission.Id;
            }
        }

        public void AddPermissionIds(long roleId, long[] permissionIds)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<RoleEntity> roleCs = new CommonService<RoleEntity>(dbc);
                var role = roleCs.GetAll().Include(r => r.Permissions).SingleOrDefault(r => r.Id == roleId);
                if (role == null)
                {
                    throw new ArgumentException("roleId=" + roleId + "的数据不存在");
                }
                role.Permissions.Clear();
                CommonService<PermissionEntity> cs = new CommonService<PermissionEntity>(dbc);
                var pms = cs.GetAll().Where(p => permissionIds.Contains(p.Id)).ToArray();
                foreach (var pm in pms)
                {
                    role.Permissions.Add(pm);
                }
                dbc.SaveChanges();
            }
        }

        public PermissionDTO[] GetAll()
        {
            throw new NotImplementedException();
        }

        public PermissionDTO GetById(long id)
        {
            throw new NotImplementedException();
        }

        public PermissionDTO GetByName(string name)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<PermissionEntity> cs = new CommonService<PermissionEntity>(dbc);
                var pm = cs.GetAll().SingleOrDefault(p => p.Name == name);
                if (pm==null)
                {
                    return null;
                }
                return new PermissionDTO { Id = pm.Id, Name = pm.Name, Description = pm.Description, CreateDateTime = pm.CreateDateTime };
            }
        }

        public PermissionDTO[] GetByRoleId(long roleId)
        {
            throw new NotImplementedException();
        }

        public bool MarkDeleted(long id)
        {
            throw new NotImplementedException();
        }

        public void UpdatePermission(long id, string name, string description)
        {
            throw new NotImplementedException();
        }

        public void UpdatePermissionIds(long roleId, long[] permissionIds)
        {
            throw new NotImplementedException();
        }
    }
}
