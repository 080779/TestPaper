using Chat.IService.Interface;
using Chat.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.DTO.DTO;

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
                user.PassCount = 0;
                user.WinCount = 0;
                user.IsWon = false;
                dbc.Users.Add(user);
                dbc.SaveChanges();
                return user.Id;
            }
        }
        public UserDTO ToDTO(UserEntity entity)
        {
            UserDTO dto = new UserDTO();
            dto.Address = entity.Address;
            dto.CreateDateTime = entity.CreateDateTime;
            dto.Gender = entity.Gender;
            dto.Id = entity.Id;
            dto.LastLoginErrorDateTime = entity.LastLoginErrorDateTime;
            dto.LoginErrorTimes = entity.LoginErrorTimes;
            dto.Mobile = entity.Mobile;
            dto.Name = entity.Name;
            dto.NickName = entity.NickName;
            dto.PhotoUrl = entity.PhotoUrl;
            dto.PassCount = entity.PassCount;
            dto.WinCount = entity.WinCount;
            dto.IsWon = entity.IsWon;
            return dto;
        }
        public UserDTO[] GetAll()
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<UserEntity> cs = new CommonService<UserEntity>(dbc);
                return cs.GetAll().ToList().Select(u=>ToDTO(u)).ToArray();
            }
        }
    }
}
