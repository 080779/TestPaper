﻿using Chat.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Service.ModelConfig
{
    class AdminUserConfig : EntityTypeConfiguration<AdminUserEntity>
    {
        /// <summary>
        /// 配置后台用户实体类对应的数据库表、关联表结构、字段属性
        /// </summary>
        public AdminUserConfig()
        {
            ToTable("T_AdminUsers");
            //WillCascadeOnDelete(false) 没有在这代码，有外键约束的时候。当删除外键会把主键的数据删除
            //IsUnicode(false) 加上生成varchar类型，否则是nvarchar类型
            //HasOptional(u => u.City).WithMany().HasForeignKey(u => u.CityId).WillCascadeOnDelete(false);
            HasMany(r => r.Roles).WithMany(u => u.AdminUsers).Map(m => m.ToTable("T_AdminUserRoles").MapLeftKey("AdminUserId").MapRightKey("RoleId"));
            Property(u => u.Name).IsRequired().HasMaxLength(50);
            Property(u => u.Mobile).HasMaxLength(20).IsRequired().IsUnicode(false);
            Property(u => u.PasswordSalt).HasMaxLength(20).IsRequired().IsUnicode(false);
            Property(u => u.PasswordHash).HasMaxLength(100).IsRequired().IsUnicode(false);
            Property(u => u.Email).HasMaxLength(30).IsRequired().IsUnicode(false);
        }
    }
}
