using Chat.DTO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.IService.Interface
{
    public interface ITestPaperService:IServiceSupport
    {
        long AddNew(string testTitle,long exercisesCount);
        TestPaperDTO GetById(long paperId);
        TestPaperDTO[] GetAll();
        bool Update(long id,string title);
        bool Delete(long id);
    }
}
