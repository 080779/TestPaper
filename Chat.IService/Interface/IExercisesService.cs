using Chat.DTO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.IService.Interface
{
    public interface IExercisesService:IServiceSupport
    {
        long AddNew(string title,long testPaperId,string optionA,string optionB,string optionC,string optionD,long rightKeyId);
        long GetPaperExercisesCount(long testPaperId);
        ExercisesDTO GetById(long id);
        ExercisesDTO[] GetExercisesByPaperId(long testPaperId);
        bool DelExercisesById(long id);
        bool Update(long id,string title, string optionA, string optionB, string optionC, string optionD, long rightKeyId);
        bool IsRightOrWrong(long paperId, long id, long rightKeyId);
    }
}
