using Chat.Service.Entities;
using System.Data.Entity.ModelConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Service.ModelConfig
{
    class ExercisesConfig:EntityTypeConfiguration<ExercisesEntity>
    {
        public ExercisesConfig()
        {
            ToTable("T_Exercises");

            Property(e => e.Title).HasMaxLength(256).IsRequired();
            HasRequired(e => e.TestPaper).WithMany().HasForeignKey(e => e.TestPaperId).WillCascadeOnDelete(false);
            Property(e => e.OptionA).HasMaxLength(1024).IsRequired();
            Property(e => e.OptionB).HasMaxLength(1024).IsRequired();
            Property(e => e.OptionC).HasMaxLength(1024).IsRequired();
            Property(e => e.OptionD).HasMaxLength(1024).IsRequired();
            HasRequired(e => e.RightKey).WithMany().HasForeignKey(e => e.RightKeyId).WillCascadeOnDelete(false);
        }
    }
}
