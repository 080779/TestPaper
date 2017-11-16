using Chat.DTO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.IService.Interface
{
    public interface IActivityService:IServiceSupport
    {
        long AddNew(string name,string description, long statusId, string imgUrl,DateTime startTime,DateTime examEndTime,DateTime rewardTime,long paperId,string prizeName,string prizeImgUrl);
        ActivityDTO[] GetAll();
        ActivityDTO GetNew();
    }
}
