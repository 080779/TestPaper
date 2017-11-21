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
        UserDTO[] Search(bool? gender,bool? isWon,DateTime? startTime,DateTime? endTime,string keyWord);
        UserDTO[] GetByActivityId(long id);
        UserDTO[] GetUsersByActivityId(long id);
        UserDTO[] PrizeSearch(long id, DateTime? startTime, DateTime? endTime, string keyWord);
        bool SetWon(long id);
        UserDTO[] GetByActivityIdHavePrize(long id);
        UserDTO[] GetByActivityIdIsWon(long id);
        bool UserIsWonByMobile(string mobile);
        bool UpdateUser(string mobile, string name, bool gender, string address);
        bool RetSetWon(long id);
        bool IsHavePrizeChance(long id);
        bool ReSetPrizeChance(long id);
    }
}
