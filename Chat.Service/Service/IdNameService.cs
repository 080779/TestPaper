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
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<IdNameEntity> cs = new CommonService<IdNameEntity>(dbc);
                var types= cs.GetAll().Where(i => i.TypeName == typeName);
                return types.ToList().Select(i => new IdNameDTO { Id = i.Id, CreateDateTime = i.CreateDateTime, ImgUrl = i.ImgUrl, Name = i.Name, TypeName = i.TypeName }).ToArray();
            }
        }

        public IdNameDTO GetById(long id)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<IdNameEntity> cs = new CommonService<IdNameEntity>(dbc);
                var type = cs.GetAll().SingleOrDefault(i => i.Id == id);
                if(type==null)
                {
                    return null;
                }
                return new IdNameDTO { Id = type.Id, CreateDateTime = type.CreateDateTime, ImgUrl = type.ImgUrl, Name = type.Name, TypeName = type.TypeName };
            }
        }
    }
}
