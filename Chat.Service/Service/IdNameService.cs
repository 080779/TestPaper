using Chat.IService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.DTO.DTO;
using Chat.DTO;
using Chat.Service.Entities;

namespace Chat.Service.Service
{
    public class IdNameService : IIdNameService
    {
        public long AddNew(string typeName, string name, string imgUrl)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                IdNameEntity idName = new IdNameEntity();
                idName.TypeName = typeName;
                idName.Name = name;
                idName.ImgUrl = imgUrl;
                dbc.IdNames.Add(idName);
                dbc.SaveChanges();
                return idName.Id;
            }
        }

        public IdNameDTO[] GetAll()
        {
            throw new NotImplementedException();
        }

        public IdNameDTO[] GetAll(string typeName)
        {
            throw new NotImplementedException();
        }

        public IdNameDTO GetById(long id)
        {
            throw new NotImplementedException();
        }
    }
}
