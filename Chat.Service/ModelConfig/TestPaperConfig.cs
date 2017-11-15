using Chat.Service.Entities;
using System.Data.Entity.ModelConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Service.ModelConfig
{
    class TestPaperConfig:EntityTypeConfiguration<TestPaperEntity>
    {
        public TestPaperConfig()
        {
            ToTable("T_TestPapers");

            Property(t => t.TestTitle).HasMaxLength(1204).IsRequired();
        }
    }
}
