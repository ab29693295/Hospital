using Edu.Entity;
using Edu.Models;
using Edu.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edu.Service
{
    public class DiscussService : CoreServiceBase
    {
        public bool insert(string content, int cid, int request)
        {
            DiscussModel dis = new DiscussModel();
            dis.Contents = content;
            dis.CourseID = cid;
            dis.UserName = Edu.Service.LoginUserService.userName;
            dis.TrueName = LoginUserService.User.TrueName == null ? LoginUserService.userName : LoginUserService.User.TrueName;
            dis.CreateDate = DateTime.Now;
            var result = RedisHelper.Hash_Set<DiscussModel>("LiveDiscuss" + cid.ToString(), DateTime.Now.ToString(), dis);
            return result;
        }
        public bool insert(string content, int cid, string userName, string trueName)
        {

            string oID = CombHelper.GenerateNumber();
            DiscussModel dis = new DiscussModel();
            dis.Contents = content;
            dis.CourseID = cid;
            dis.UserName = userName;
            dis.TrueName = trueName;
            dis.oID = oID;
            dis.CreateDate = DateTime.Now;
            var result = RedisHelper.Hash_Set<DiscussModel>("LiveDiscuss" + cid.ToString(), DateTime.Now.ToString(), dis);
            return result;
        }
        public bool insertQK(string content, int cid,string groupID, string userName, string trueName, string oID)
        {
            DiscussModel dis = new DiscussModel();
            dis.Contents = content;
            dis.CourseID = cid;
            dis.UserName = userName;
            dis.TrueName = trueName;
            dis.GroupID = groupID;
            dis.oID = oID;
            dis.CreateDate = DateTime.Now;
            var result = RedisHelper.Hash_Set<DiscussModel>("LiveDiscuss" + cid.ToString(), oID, dis);
            return result;
        }

        public bool insertTheme(int cid, string groupName,string groupID)
        {
            DiscussGroup disGroup = new DiscussGroup();
            disGroup.cid = cid;
            disGroup.GroupName = groupName;
            disGroup.GroupID = groupID;
            var result = RedisHelper.Hash_Set<DiscussGroup>("DiscussGroup" + cid.ToString(), groupID, disGroup);
            return result;
        }



        public bool insertPass(string content, int cid,string GroupID, string userName, string trueName, string oID, int status,string CheckName)
        {
            DiscussModel dis = new DiscussModel();
            dis.Contents = content;
            dis.CourseID = cid;
            dis.UserName = userName;
            dis.TrueName = trueName;
            dis.GroupID = GroupID;
            dis.CheckName = CheckName;
            dis.oID = oID;
            dis.CreateDate = DateTime.Now;
            dis.Status = status;
            var result = RedisHelper.Hash_Set<DiscussModel>("LiveDiscuss" + cid.ToString() + "_0", oID, dis);
            return result;
        }
        public bool insertCheck(int cid,DiscussModel dis)
        {
            var result = RedisHelper.Hash_Set<DiscussModel>("LiveDiscuss" + cid.ToString() + "_Chek", CombHelper.GenerateNumber(), dis);
            return result;
        }

        /// <summary>
        /// 得到所有讨论 最多加载 50 条最新数据
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public IEnumerable<DiscussModel> GetCheckDisuss(int cid)
        {
            var list = RedisHelper.Hash_GetAll<DiscussModel>("LiveDiscuss" + cid.ToString() + "_0");
            if (list != null)
            {
                return list.OrderBy(p => p.CreateDate);
            }
            else
            {
                list = new List<DiscussModel>();
            }
            return list;
        }

        public bool insertApp(Discuss dis)
        {
            var result = RedisHelper.Hash_Set<Discuss>("LiveDiscuss" + dis.CourseID.ToString(), CombHelper.GenerateNumber(), dis);
            return result;
        }
        /// <summary>
        /// 得到所有讨论 最多加载 50 条最新数据
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public IEnumerable<DiscussModel> GetAllDisuss(int cid)
        {
            var list = RedisHelper.Hash_GetAll<DiscussModel>("LiveDiscuss" + cid.ToString());
            if (list != null)
            {
                return list.OrderBy(p => p.CreateDate).Take(50);
            }
            else
            {
                list = new List<DiscussModel>();
            }
            return list;
        }
        /// <summary>
        /// 获取所有审核过的讨论
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public IEnumerable<DiscussModel> GetAllPassDisuss(int cid)
        {
            var list = RedisHelper.Hash_GetAll<DiscussModel>("LiveDiscuss" + cid.ToString() + "_0");
            if (list != null)
            {
                return list.OrderBy(p => p.CreateDate);
            }
            else
            {
                list = new List<DiscussModel>();
            }
            return list;
        }

        public DiscussModel GetDiscuss(int cid, string oID)
        {
            var dis = RedisHelper.Hash_GetAll<DiscussModel>("LiveDiscuss" + cid.ToString()).Where(p => p.oID == oID).FirstOrDefault();
            if (dis != null)
            {
                return dis;
            }
            else
            {
                dis = new DiscussModel();
            }
            return dis;
        }

        public IEnumerable<DiscussModel> GetAppDisuss(int cid, int pageNo = 1, int pageSize = 10, long date = 0)
        {
            var list = RedisHelper.Hash_GetAll<DiscussModel>("LiveDiscuss" + cid.ToString());
            var query = list.OrderByDescending(p => p.CreateDate).Skip((pageNo - 1) * pageSize).Take(pageSize);

            if (date == 0)
            {
                query = list.OrderByDescending(p => p.CreateDate).Skip((pageNo - 1) * pageSize).Take(pageSize);
            }
            else
            {
                DateTime dt = ConvertLongDateTime(date);
                query = list.Where(p => p.CreateDate > dt).OrderByDescending(p => p.CreateDate).Skip((pageNo - 1) * pageSize).Take(pageSize);
            }
            return query.OrderByDescending(p => p.CreateDate);
        }

        public static DateTime ConvertLongDateTime(long date)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));

            DateTime dtResult = dtStart.AddSeconds(date);
            return dtResult;
        }


        public IEnumerable<DiscussModel> GetQKDisuss(int cid,string GroupID, int pageNo = 1, int pageSize = 10)
        {
            var list = RedisHelper.Hash_GetAll<DiscussModel>("LiveDiscuss" + cid.ToString() + "_0");
            if (list != null)
            {
                var query = list.Where(p => p.Status == 1 && p.GroupID == GroupID).OrderByDescending(p => p.CreateDate).Skip((pageNo - 1) * pageSize).Take(pageSize);
                query = query.OrderByDescending(p => p.CreateDate).Take(pageSize);
                return query.OrderBy(p => p.CreateDate);
            }
            return null;

        }
    }
}
