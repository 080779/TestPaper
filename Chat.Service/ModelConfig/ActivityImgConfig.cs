using Chat.Service.Entities;
using System.Data.Entity.ModelConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Service.ModelConfig
{
    class ActivityImgConfig:EntityTypeConfiguration<ActivityImgEntity>
    {
        public ActivityImgConfig()
        {
            ToTable("T_ActivityImgs");
                        
            Property(a => a.Description).IsRequired();
            Property(a => a.ImgUrl).HasMaxLength(1024).IsRequired();
            HasRequired(a => a.Activity).WithMany().HasForeignKey(a => a.ActivityId).WillCascadeOnDelete(false);
        }
    }
}
