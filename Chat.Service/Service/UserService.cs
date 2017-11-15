using Chat.IService.Interface;
using Chat.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Service.Service
{
    public class UserService : IUserService
    {
        public long AddNew(string name, string nickName, string photoUrl, string mobile, bool gender, string address)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<UserEntity> cs = new CommonService<UserEntity>(dbc);
                if(cs.GetAll().Any(u=>u.Mobile==mobile))
                {
                    return -1;
                }
                UserEntity user = new UserEntity();
                user.Mobile = mobile;
                user.Name = name;
                user.NickName = nickName;
                user.PhotoUrl = photoUrl;
                user.Gender = gender;
                user.Address = address;
                user.LoginErrorTimes = 0;
                user.PasswordHash = "";
                user.PasswordSalt = "";
                dbc.Users.Add(user);
                dbc.SaveChanges();
                return user.Id;
            }
        }
    }
}
