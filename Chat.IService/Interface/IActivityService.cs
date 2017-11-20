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
        ActivityDTO[] GetPageData(int pageSize, int currentIndex);
        ActivityDTO GetNew();
        ActivityDTO GetById(long id);
        ActivityDTO GetByStatus(string statusName);
        bool Update(long id, string name, string description, long statusId, string imgUrl, DateTime startTime, DateTime examEndTime, DateTime rewardTime, long paperId, string prizeName, string prizeImgUrl);
        ActivityDTO[] Search(long? statusId, DateTime? startTime, DateTime? endTime, string keyWord);
        bool Delete(long id);
        bool AddUserId(long activityId, long userId);
        ActivityDTO[] GetActivityByUserId(long id);
        ActivityDTO[] GetByUserId(long id);
        long GetTotalCount();
        bool CheckByStatusId(long id,long statusId);
        bool CheckByStatusId(long statusId);
        bool CheckByPaperId(long id);
        /// <summary>
        /// 更新访问次数等
        /// </summary>
        /// <param name="id"></param>
        /// <param name="setVisitCount">访问次数</param>
        /// <param name="setForwardCount">转发次数</param>
        /// <param name="setAnswerCount">答题人数</param>
        /// <param name="setHavePrizeCount">获奖资格人数</param>
        /// <param name="setPrizeCount">获奖人数</param>
        /// <returns></returns>
        bool UpdateCount(long id, bool setVisitCount, bool setForwardCount, bool setAnswerCount, bool setHavePrizeCount, bool setPrizeCount);
        bool CheckHaveStatusId(long id);
    }
}
