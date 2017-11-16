using Chat.Service.Entities;
using System.Data.Entity.ModelConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Service.ModelConfig
{
    class ActivityConfig:EntityTypeConfiguration<ActivityEntity>
    {
        public ActivityConfig()
        {
            ToTable("T_Activities");

            Property(a => a.Name).HasMaxLength(30).IsRequired();
            Property(a => a.Description).IsRequired();
            Property(a => a.WeChatUrl).HasMaxLength(256).IsUnicode(false);
            Property(a => a.PrizeName).HasMaxLength(50).IsRequired();
            Property(a => a.PrizeImgUrl).HasMaxLength(100).IsRequired().IsUnicode(false);
            Property(a => a.ImgUrl).HasMaxLength(100).IsUnicode(false);
            HasRequired(a => a.Status).WithMany().HasForeignKey(a => a.StatusId).WillCascadeOnDelete(false);
            HasRequired(a => a.Papers).WithMany().HasForeignKey(a => a.PaperId).WillCascadeOnDelete(false);
        }
    }
}
