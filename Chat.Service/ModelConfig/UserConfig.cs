using Chat.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Service.ModelConfig
{
    class UserConfig:EntityTypeConfiguration<UserEntity>
    {
        public UserConfig()
        {

            ToTable("T_Users");

            Property(u => u.Name).HasMaxLength(50).IsRequired();
            Property(u => u.NickName).HasMaxLength(100).IsRequired();
            Property(u => u.PhotoUrl).HasMaxLength(1024).IsRequired();
            Property(u => u.Mobile).HasMaxLength(100).IsRequired().IsUnicode(false);
            Property(u => u.Address).HasMaxLength(1024).IsRequired();
            HasMany(a => a.Activities).WithMany(u => u.Users).Map(m => m.ToTable("T_UserActivities").MapLeftKey("UserId").MapRightKey("ActivityId"));
            Property(u => u.PasswordHash).HasMaxLength(100).IsRequired();
            Property(u => u.PasswordSalt).HasMaxLength(20).IsRequired();
        }
    }
}
