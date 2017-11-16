using Chat.DTO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.IService.Interface
{
    public interface IUserService:IServiceSupport
    {
        long AddNew(string name,string nickName,string photoUrl,string mobile,bool gender,string address);
        UserDTO[] GetAll();
    }
}
