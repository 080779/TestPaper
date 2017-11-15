using Chat.DTO;
using Chat.DTO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.IService.Interface
{
    public interface IIdNameService:IServiceSupport
    {
        long AddNew(string typeName, string name, string imgUrl);
        IdNameDTO GetById(long id);
        IdNameDTO[] GetAll(string typeName);
        IdNameDTO[] GetAll();
    }
}
