using Chat.IService.Interface;
using Chat.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.DTO.DTO;
using System.Data.SqlClient;
using Chat.WebCommon;

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
                user.NickName = "dt_" + new Random().Next();
                user.PhotoUrl = "";
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
            dto.ChangeTime = entity.ChangeTime;
            return dto;
        }
                
        public UserDTO[] GetAll()
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<UserEntity> cs = new CommonService<UserEntity>(dbc);
                return cs.GetAll().Take(10).ToList().Select(u=>ToDTO(u)).ToArray();
            }
        }

        public UserDTO[] GetByActivityId(long id)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<ActivityEntity> cs = new CommonService<ActivityEntity>(dbc);
                CommonService<UserEntity> ucs = new CommonService<UserEntity>(dbc);
                var activity = cs.GetAll().SingleOrDefault(a => a.Id == id);
                if (activity == null)
                {
                    return null;
                }

                return dbc.Database.SqlQuery<UserDTO>("select top(20) u.Id,u.Name,u.NickName,u.PhotoUrl,u.Mobile,u.Gender,u.Address,u.PasswordHash,u.PasswordSalt,u.LoginErrorTimes,u.LastLoginErrorDateTime,u.PassCount,u.WinCount,u.IsWon,u.IsDeleted,u.ChangeTime,u.CreateDateTime from T_Users as u, (select UserId from T_UserActivities where ActivityId=@id) as a where a.UserId=u.Id and u.IsDeleted=0", new SqlParameter("@id", id)).ToArray();
            }
        }

        /// <summary>
        /// 根据活动id查找参与活动的用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserSearchResult GetUsersByActivityId(long id, int currentIndex, int pageSize)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<ActivityEntity> cs = new CommonService<ActivityEntity>(dbc);
                CommonService<UserEntity> ucs = new CommonService<UserEntity>(dbc);
                var activity = cs.GetAll().SingleOrDefault(a => a.Id == id);
                if (activity == null)
                {
                    return null;
                }
                var users = from u in dbc.Users
                            from a in u.Activities
                            where a.Id == id && u.IsDeleted==false
                            select u;
                UserSearchResult result = new UserSearchResult();
                result.TotalCount = users.Count();
                result.Users= users.OrderByDescending(u=>u.CreateDateTime).Skip(currentIndex).Take(pageSize).ToList().Select(u => ToDTO(u)).ToArray();
                return result;
            }
        }

        public UserDTO[] GetByActivityIdHavePrize1(long id)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<ActivityEntity> cs = new CommonService<ActivityEntity>(dbc);
                var activity = cs.GetAll().SingleOrDefault(a => a.Id == id);
                if (activity == null)
                {
                    return null;
                }
                return dbc.Database.SqlQuery<UserDTO>("select u.Id,u.Name,u.NickName,u.PhotoUrl,u.Mobile,u.Gender,u.Address,u.PasswordHash,u.PasswordSalt,u.LoginErrorTimes,u.LastLoginErrorDateTime,u.PassCount,u.WinCount,u.IsWon,u.IsDeleted,u.ChangeTime,u.CreateDateTime from T_Users as u, (select UserId from T_UserActivities where ActivityId=@id) as a where a.UserId=u.Id and u.LoginErrorTimes=1 and u.IsDeleted=0", new SqlParameter("@id", id)).ToArray();
            }
        }

        /// <summary>
        /// 根据活动id查找出该活动有获奖资格的用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserSearchResult GetByActivityIdHavePrize(long id, int currentIndex, int pageSize)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<ActivityEntity> cs = new CommonService<ActivityEntity>(dbc);
                CommonService<UserEntity> ucs = new CommonService<UserEntity>(dbc);
                var activity = cs.GetAll().SingleOrDefault(a => a.Id == id);
                if (activity == null)
                {
                    return null;
                }
                var users = from u in dbc.Users
                            from a in u.Activities
                            where a.Id == id && u.IsDeleted == false && u.LoginErrorTimes==1
                            select u;
                UserSearchResult result = new UserSearchResult();
                result.TotalCount = users.Count();
                result.Users = users.OrderByDescending(u=>u.CreateDateTime).Skip(currentIndex).Take(pageSize).ToList().Select(u => ToDTO(u)).ToArray();
                return result;
            }
        }

        public UserDTO[] GetByActivityIdIsWon1(long id)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<ActivityEntity> cs = new CommonService<ActivityEntity>(dbc);
                var activity = cs.GetAll().SingleOrDefault(a => a.Id == id);
                if (activity == null)
                {
                    return null;
                }
                return dbc.Database.SqlQuery<UserDTO>("select u.Id,u.Name,u.NickName,u.PhotoUrl,u.Mobile,u.Gender,u.Address,u.PasswordHash,u.PasswordSalt,u.LoginErrorTimes,u.LastLoginErrorDateTime,u.PassCount,u.WinCount,u.IsWon,u.IsDeleted,u.ChangeTime,u.CreateDateTime from T_Users as u, (select UserId from T_UserActivities where ActivityId=@id) as a where a.UserId=u.Id and u.IsWon=1 and u.IsDeleted=0", new SqlParameter("@id", id)).ToArray();
            }
        }

        /// <summary>
        /// 通过活动id查询出已经获奖的用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="currentIndex">跳过的条数（（当前页数-1）*每页数）</param>
        /// <param name="pageSize">每页数</param>
        /// <returns></returns>
        public UserSearchResult GetByActivityIdIsWon(long id, int currentIndex, int pageSize)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<ActivityEntity> cs = new CommonService<ActivityEntity>(dbc);
                var activity = cs.GetAll().SingleOrDefault(a => a.Id == id);
                if (activity == null)
                {
                    return null;
                }
                var users = from u in dbc.Users
                            from a in u.Activities
                            where a.Id == id && u.IsDeleted == false && u.IsWon==true
                            select u;
                UserSearchResult result = new UserSearchResult();
                result.TotalCount = users.Count();
                result.Users = users.OrderByDescending(u=>u.CreateDateTime).Skip(currentIndex).Take(pageSize).ToList().Select(u => ToDTO(u)).ToArray();
                return result;
            }
        }

        public UserDTO[] PrizeSearch1(long id,DateTime? startTime,DateTime? endTime,string keyWord, int currentIndex, int pageSize)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<ActivityEntity> cs = new CommonService<ActivityEntity>(dbc);
                var activity = cs.GetAll().SingleOrDefault(a => a.Id == id);
                if (activity == null)
                {
                    return null;
                }
                string sql= "select top(10) u.Id,u.Name,u.NickName,u.PhotoUrl,u.Mobile,u.Gender,u.Address,u.PasswordHash,u.PasswordSalt,u.LoginErrorTimes,u.LastLoginErrorDateTime,u.PassCount,u.WinCount,u.IsWon,u.IsDeleted,u.ChangeTime,u.CreateDateTime from T_Users as u, (select UserId from T_UserActivities where ActivityId = @id) as a where a.UserId = u.Id and u.IsDeleted=0";
                if (startTime!=null)
                {
                     sql= sql+" and u.ChangeTime >= @startTime";
                }
                if(endTime!=null)
                {
                    sql = sql+" and u.ChangeTime<=@endTime";
                }
                long intA;
                if (!string.IsNullOrEmpty(keyWord) && !long.TryParse(keyWord, out intA))
                {
                    sql = sql + " and u.Name like '%'+@keyWord+'%'";
                }
                if(!string.IsNullOrEmpty(keyWord) && long.TryParse(keyWord, out intA))
                {
                    sql = sql + " and u.Mobile like '%'+@keyWord+'%'";
                }                
                if (sql.Contains("@startTime") && sql.Contains("@endTime") && sql.Contains("@keyWord"))
                {
                    return dbc.Database.SqlQuery<UserDTO>(sql, new SqlParameter("@id", id), new SqlParameter("@startTime", startTime), new SqlParameter("@endTime", endTime), new SqlParameter("@keyWord", keyWord)).ToArray();
                }
                if(sql.Contains("@startTime") && sql.Contains("@endTime"))
                {
                    return dbc.Database.SqlQuery<UserDTO>(sql, new SqlParameter("@id", id), new SqlParameter("@startTime", startTime), new SqlParameter("@endTime", endTime)).ToArray();
                }
                if(sql.Contains("@endTime") && sql.Contains("@keyWord"))
                {
                    return dbc.Database.SqlQuery<UserDTO>(sql, new SqlParameter("@id", id), new SqlParameter("@endTime", endTime), new SqlParameter("@keyWord", keyWord)).ToArray();
                }
                if(sql.Contains("@startTime") && sql.Contains("@keyWord"))
                {
                    return dbc.Database.SqlQuery<UserDTO>(sql, new SqlParameter("@id", id), new SqlParameter("@startTime", startTime), new SqlParameter("@keyWord", keyWord)).ToArray();
                }
                if(sql.Contains("@startTime"))
                {
                    return dbc.Database.SqlQuery<UserDTO>(sql, new SqlParameter("@id", id), new SqlParameter("@startTime", startTime)).ToArray();
                }
                if(sql.Contains("@endTime"))
                {
                    return dbc.Database.SqlQuery<UserDTO>(sql, new SqlParameter("@id", id), new SqlParameter("@endTime", endTime)).ToArray();
                }
                if (sql.Contains("@keyWord"))
                {
                    return dbc.Database.SqlQuery<UserDTO>(sql, new SqlParameter("@id", id), new SqlParameter("@keyWord", keyWord)).ToArray();
                }
                return dbc.Database.SqlQuery<UserDTO>(sql, new SqlParameter("@id", id)).ToArray();
            }
        }

        //public UserSearchResult PrizeSearch(long id, DateTime? startTime, DateTime? endTime, string keyWord, int currentIndex, int pageSize)
        //{

        //}

        public UserSearchResult Search(bool? gender, bool? isWon, DateTime? startTime, DateTime? endTime, string keyWord, int currentIndex, int pageSize)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<UserEntity> cs = new CommonService<UserEntity>(dbc);
                var items = cs.GetAll();
                if(gender!=null)
                {
                    items = items.Where(u => u.Gender == gender);
                }
                if(isWon!=null)
                {
                    items = items.Where(u => u.IsWon == isWon);
                }
                if (startTime != null)
                {
                    startTime = DateTimeHelper.GetBeginDate((DateTime)startTime);
                    items = items.Where(u => u.CreateDateTime >= startTime);
                }
                if (endTime != null)
                {
                    endTime = DateTimeHelper.GetEndDate((DateTime)endTime);
                    items = items.Where(u => u.CreateDateTime <= endTime);
                }
                if (keyWord != null)
                {
                    items = items.Where(u => u.Name.Contains(keyWord) || u.Mobile.Contains(keyWord));
                }
                UserSearchResult result = new UserSearchResult();
                result.TotalCount = items.Count();
                result.Users= items.Skip(currentIndex).Take(pageSize).ToList().Select(u => ToDTO(u)).ToArray();
                return result;
            }
        }

        public bool SetWon(long id)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<UserEntity> cs = new CommonService<UserEntity>(dbc);
                var user=cs.GetAll().SingleOrDefault(u => u.Id == id);
                if(user==null)
                {
                    return false;
                }
                user.IsWon = true;
                user.WinCount++;
                dbc.SaveChanges();
                return true;
            }
        }

        public bool RetSetWon(long id)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<UserEntity> cs = new CommonService<UserEntity>(dbc);
                var user = cs.GetAll().SingleOrDefault(u => u.Id == id);
                if (user == null)
                {
                    return false;
                }
                user.IsWon = false;
                dbc.SaveChanges();
                return true;
            }
        }

        public bool UserIsWonByMobile(string mobile)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<UserEntity> cs = new CommonService<UserEntity>(dbc);
                if(string.IsNullOrEmpty(mobile))
                {
                    return false;
                }
                var user= cs.GetAll().SingleOrDefault(u=>u.Mobile==mobile);
                if(user==null)
                {
                    return false;
                }
               return user.IsWon == true;
            }
        }

        public bool UpdateUser(string mobile,string name,bool gender,string address)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<UserEntity> cs = new CommonService<UserEntity>(dbc);
                UserEntity user= cs.GetAll().SingleOrDefault(u=>u.Mobile==mobile);
                if(user==null)
                {
                    return false;
                }
                user.Name = name;
                user.Gender = gender;
                user.Address = address;
                dbc.SaveChanges();
                return true;
            }
        }

        public bool IsHavePrizeChance(long id)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<UserEntity> cs = new CommonService<UserEntity>(dbc);
                UserEntity user= cs.GetAll().SingleOrDefault(u=>u.Id==id);
                if(user==null)
                {
                    return false;
                }
                user.LoginErrorTimes = 1;
                dbc.SaveChanges();
                return true;
            }
        }

        public bool ReSetPrizeChance(long id)
        {
            using (MyDbContext dbc = new MyDbContext())
            {
                CommonService<UserEntity> cs = new CommonService<UserEntity>(dbc);
                UserEntity user = cs.GetAll().SingleOrDefault(u => u.Id == id);
                if (user == null)
                {
                    return false;
                }
                user.LoginErrorTimes = 0;
                dbc.SaveChanges();
                return true;
            }
        }
    }
}
