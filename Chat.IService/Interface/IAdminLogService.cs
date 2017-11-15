using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.IService.Interface
{
    public interface IAdminLogService:IServiceSupport
    {
        long AddNew(long adminUserId,string message);
    }
}
