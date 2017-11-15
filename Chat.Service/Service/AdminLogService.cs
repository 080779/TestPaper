using Chat.IService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.DTO.DTO;
using Chat.Service.Entities;

namespace Chat.Service.Service
{
    public class AdminLogService : IAdminLogService
    {
        public long AddNew(long adminUserId, string message)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                AdminLogEntity adminLog = new AdminLogEntity();
                adminLog.AdminUserId = adminUserId;
                adminLog.Message = message;
                dbc.AdminLogs.Add(adminLog);
                dbc.SaveChanges();
                return adminLog.Id;
            }
        }
    }
}
