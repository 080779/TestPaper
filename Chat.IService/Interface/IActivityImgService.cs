using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.IService.Interface
{
    public interface IActivityImgService:IServiceSupport
    {
        long AddNew(long activityId,string description,string imgUrl);
    }
}
