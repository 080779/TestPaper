using Chat.Service.Entities;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Service
{
    /// <summary>
    /// EntityFramework操作类
    /// </summary>
    class MyDbContext : DbContext
    {
        private static ILog log = LogManager.GetLogger(typeof(MyDbContext));

        public MyDbContext() : base("name=connStr") //“connStr”数据库连接字符串
        {
            //Database.SetInitializer<MyDbContext>(null);
            this.Database.Log = sql => log.DebugFormat("EF执行SQL：{0}", sql);//用log4NET记录数据操作日志
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<ActivityImgEntity> ActivityImgs { get; set; }
        public DbSet<ActivityEntity> Activities { get; set; }
        public DbSet<AdminLogEntity> AdminLogs { get; set; }
        public DbSet<AdminUserEntity> AdminUsers { get; set; }
        public DbSet<CityEntity> Cities { get; set; }
        public DbSet<ExercisesEntity> Exercises { get; set; }
        public DbSet<IdNameEntity> IdNames { get; set; }
        public DbSet<PermissionEntity> Permissions { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<SettingEntity> Settings { get; set; }
        public DbSet<TestPaperEntity> TestPapers { get; set; }
        public DbSet<UserEntity> Users { get; set; }        
    }
}
