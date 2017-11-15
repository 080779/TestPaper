using Chat.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Service.ModelConfig
{
    class IdNameConfig:EntityTypeConfiguration<IdNameEntity>
    {
        public IdNameConfig()
        {
            ToTable("T_IdNames");
            Property(i => i.TypeName).HasMaxLength(1024).IsRequired();
            Property(i => i.Name).HasMaxLength(1024).IsRequired();
            Property(i => i.ImgUrl).HasMaxLength(100);
        }
    }
}
