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
    public class RoleService : IRoleService
    {
        public long AddNew(string roleName,string description)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<RoleEntity> cs = new CommonService<RoleEntity>(dbc);
                if(cs.GetAll().Any(r=>r.Name==roleName))
                {
                    return -1;
                }
                RoleEntity role = new RoleEntity();
                role.Name = roleName;
                role.Description = description;
                dbc.Roles.Add(role);
                dbc.SaveChanges();
                return role.Id;
            }
        }

        public void AddRoleIds(long adminUserId, long[] roleIds)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<AdminUserEntity> cs = new CommonService<AdminUserEntity>(dbc);
                //不能有.AsNoTracking()，加了的数据不能修改
                var user = cs.GetAll().Include(u => u.Roles).SingleOrDefault(u => u.Id == adminUserId);
                //var user = cs.GetById(adminUserId);
                if (user == null)
                {
                    throw new ArgumentException("adminUserId=" + adminUserId + "的数据已经存在");
                }
                CommonService<RoleEntity> roleCs = new CommonService<RoleEntity>(dbc);
                var roles = roleCs.GetAll().Where(r => roleIds.Contains(r.Id)).ToArray();
                foreach (var role in roles)
                {
                    user.Roles.Add(role);
                }
                dbc.SaveChanges();
            }
        }

        public RoleDTO[] GetAll()
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<RoleEntity> cs = new CommonService<RoleEntity>(dbc);
                return cs.GetAll().AsNoTracking().Select(r=>new RoleDTO {Id=r.Id,Name=r.Name,Description=r.Description,CreateDateTime=r.CreateDateTime }).ToArray();
            }
        }

        public RoleDTO[] GetByAdminUserId(long adminUserId)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<AdminUserEntity> cs = new CommonService<AdminUserEntity>(dbc);
                AdminUserEntity user= cs.GetAll().Include(u => u.Roles).AsNoTracking().SingleOrDefault(u => u.Id == adminUserId);
                if(user==null)
                {
                    return null;
                }
                return user.Roles.Select(r => new RoleDTO { Id = r.Id, Name = r.Name, Description = r.Description, CreateDateTime = r.CreateDateTime}).ToArray();
            }
        }

        public RoleDTO GetById(long id)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<RoleEntity> cs = new CommonService<RoleEntity>(dbc);
                RoleEntity role = cs.GetAll().SingleOrDefault(r => r.Id == id);
                if(role==null)
                {
                    return null;
                }
                return new RoleDTO { Id = role.Id,Name=role.Name, Description = role.Description, CreateDateTime =role.CreateDateTime };
            }
        }

        public RoleDTO GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public bool MarkDeleted(long roleId)
        {
            throw new NotImplementedException();
        }

        public void Update(long roleId, string roleName,string description)
        {
            throw new NotImplementedException();
        }

        public void UpdateRoleIds(long adminUserId, long[] roleIds)
        {
            throw new NotImplementedException();
        }
    }
}
