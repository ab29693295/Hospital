using Edu.Entity;
using Edu.Models;
using Edu.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Edu.Service
{
    public class LiveUserService : CoreServiceBase
    {

        /// <summary>
        /// 最多加载100条数据
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="host"></param>
        /// <param name="TotolCount"></param>
        /// <param name="CurOnLine"></param>
        /// <returns></returns>
        public IEnumerable<LiveUserOnLine> GetAllCatch(int cid, string host, ref int TotolCount, ref int CurOnLine)
        {
            var list = RedisHelper.Hash_GetAll<LiveUserOnLine>("LiveUserOnLine" + cid.ToString()).Where(p => p.UserName != host).OrderBy(p => p.Date);
            TotolCount = list.Count();
            CurOnLine = list.Where(p => p.isOnLine == 1).Count();
            var query = list.OrderByDescending(p => p.state).OrderByDescending(p => p.isOnLine).ThenByDescending(p => p.Date).Take(100);
            return query;

        }
        private bool isOnLineByDate(DateTime dt)
        {
            double LoginLong = this.DateDiff(DateTime.Now, dt);//登录时间
            if (LoginLong >= 10)
            {//如果超过5秒则已经下线
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 得到该课程下的所有人
        /// 默认加载游客
        /// </summary>
        /// <returns></returns>
      
        public IEnumerable<LiveUsers> GetAllSql(int cid, string host, ref int iCount)
        {
            List<LiveUsers> list = new List<LiveUsers>();

            string sqlstr = string.Format(@"select 
ID,
a.UserName,
(CASE WHEN ISNULL(b.TrueName) THEN a.UserName ELSE b.TrueName END) as TrueName,
isHost,
isHand,
isActive,
ActiveStartDate,
ActiveEndDate,
a.Stream,
GuestIP,
0 isOnLine 
from liveuser a 
LEFT JOIN userinfo b on a.username =b.username 
where a.courseid={0} and a.username!='{1}'", cid, host);
            var date = DateTime.Now;
            using (var db = new Edu.Entity.EduContext())
            {
                var ss = db.Database.SqlQuery<LiveUsers>(sqlstr);
                iCount = ss.Count();

                foreach (var item in ss.ToList())
                {
                    item.isOnLine = this.IsOnLine(cid, item.UserName, item.GuestIP);
                    list.Add(item);
                }

            }
            LogHelper.Info(string.Format("foreach({0}),  Date:{1},duration:{2}",
       cid,
       DateTime.Now,
       new DateTimeHelper().DateDiffTimeSpan(DateTime.Now, date).TotalMilliseconds));
            return list;
        }


        /// <summary>
        /// 得到该课程下的所有人
        /// 默认加载游客
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LiveUsers> GetAll(int cid, string host)
        {
            IEnumerable<LiveUser> list = unitOfWork.DLiveUser.Get(p => p.CourseID == cid && p.UserName != host);
            var query = from m in list
                        join n in unitOfWork.DUserInfo.GetAll() on m.UserName equals n.UserName into temp
                        from a in temp.DefaultIfEmpty()
                        select new LiveUsers
                        {
                            ID = m.ID,
                            UserName = m.UserName,
                            TrueName = (a != null && !string.IsNullOrEmpty(a.TrueName)) ? a.TrueName : m.UserName,
                            isHost = m.isHost,
                            isHand = m.isHand,
                            isActive = m.isActive,
                            ActiveStartDate = m.ActiveStartDate,
                            ActiveEndDate = m.ActiveEndDate,
                            Stream = m.Stream,
                            isOnLine = this.IsOnLine(cid, m.UserName, m.GuestIP)
                        }; return query;
        }
        /// <summary>
        /// 判断是否在线
        /// 改为 redis存储 存储名称为 LiveUserOnLine+cid
        /// </summary>
        /// <returns></returns>
        public bool IsOnLine(int cid, string username, string guestip = "")
        {
            if (!string.IsNullOrEmpty(guestip))
            {///游客
                var list = RedisHelper.Hash_GetAll<LiveUserOnLine>("LiveUserOnLine" + cid.ToString());
                //解决历史数据问题
                if (list == null)
                {
                    LoginXMl2(cid, username, username, guestip);
                }
                var gp = list.FirstOrDefault(p => p.IP == guestip);
                if (gp == null)
                {
                    return false;
                }
                else
                {
                    double LoginLong = this.DateDiff(DateTime.Now, gp.Date);//登录时间
                    if (LoginLong >= 10)
                    {//如果超过5秒则已经下线
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

            }
            else
            {//非游客
                var query = RedisHelper.Hash_Get<LiveUserOnLine>("LiveUserOnLine" + cid.ToString(), username);
                if (query == null) return false;
                else
                {
                    double LoginLong = this.DateDiff(DateTime.Now, query.Date);//登录时间
                    if (LoginLong >= 12)
                    {//如果超过5秒则已经下线
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

            }

        }
        private double DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            double dateDiff = 0;
            try
            {
                TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
                TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
                TimeSpan ts = ts1.Subtract(ts2).Duration();
                dateDiff = ts.TotalSeconds;
            }
            catch
            {

            }
            return dateDiff;
        }
        /// <summary>
        /// 第一次进入时候插入在线
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="user"></param>
        public void InsertFirst(int cid, string userName, string trueName)
        {
            string livestream = Edu.Tools.CombHelper.GenerateLong().ToString();
            LiveUser lv = new LiveUser();
            lv.CourseID = cid;
            lv.UserName = userName;
            lv.TrueName = trueName;
            lv.Stream = livestream;
            lv.isActive = 0;
            lv.ActiveStartDate = DateTime.Now;
            var liveuser = unitOfWork.DLiveUser.Get(p => p.UserName == lv.UserName && p.CourseID == cid).FirstOrDefault();
            if (liveuser == null)
            {
                unitOfWork.DLiveUser.Insert(lv);
                unitOfWork.Save();
            }
        }

        /// <summary>
        /// 第一次进入时候插入在线
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="user"></param>
        public void InsertFirst(int cid, UserInfo user)
        {
            if (user == null)
                return;
            string livestream = Edu.Tools.CombHelper.GenerateLong().ToString();
            LiveUser lv = new LiveUser();
            lv.CourseID = cid;
            lv.UserName = user.UserName;
            lv.TrueName = user.TrueName;
            lv.Stream = livestream;
            lv.isActive = 0;
            lv.ActiveStartDate = DateTime.Now;
            var liveuser = unitOfWork.DLiveUser.Get(p => p.UserName == lv.UserName && p.CourseID == cid).FirstOrDefault();
            if (liveuser == null)
            {
                unitOfWork.DLiveUser.Insert(lv);
                unitOfWork.Save();
            }
        }

        /// <summary>
        /// 用户进入时操作redis
        /// </summary>
        public void LoginXMl(int cid, string uName, string guestip = "")
        {
            if (!string.IsNullOrEmpty(guestip))
            { //游客身份登录
                var list = RedisHelper.Hash_GetAll<LiveUserOnLine>("LiveUserOnLine" + cid.ToString());
                if (list == null)
                {
                    LiveUserOnLine lu = new LiveUserOnLine()
                    {
                        UserName = uName,
                        Date = DateTime.Now,
                        ConnectionId = "",
                        isOnLine = 1,
                        state = 0,
                        stateDate = DateTime.Now,
                        SteamID = "",
                        IP = guestip
                    };
                    RedisHelper.Hash_Set<LiveUserOnLine>("LiveUserOnLine" + cid.ToString(), uName, lu);
                }
                else
                {
                    var gp = list.FirstOrDefault(p => p.IP == HttpContext.Current.Session.SessionID);
                    if (gp != null)
                    {
                        gp.Date = DateTime.Now;
                        RedisHelper.Hash_Remove("LiveUserOnLine" + cid.ToString(), gp.UserName);
                        RedisHelper.Hash_Set<LiveUserOnLine>("LiveUserOnLine" + cid.ToString(), uName, gp);
                    }
                    else
                    {
                        LiveUserOnLine lu = new LiveUserOnLine()
                        {
                            UserName = uName,
                            Date = DateTime.Now,
                            ConnectionId = "",
                            isOnLine = 1,
                            state = 0,
                            stateDate = DateTime.Now,
                            SteamID = "",
                            IP = guestip
                        };
                        RedisHelper.Hash_Set<LiveUserOnLine>("LiveUserOnLine" + cid.ToString(), uName, lu);
                    }
                }
            }
            else
            {//登录用户
                var query = RedisHelper.Hash_Get<LiveUserOnLine>("LiveUserOnLine" + cid.ToString(), uName);
                if (query == null)
                {
                    LiveUserOnLine lu = new LiveUserOnLine()
                    {
                        UserName = uName,
                        Date = DateTime.Now,
                        ConnectionId = "",
                        isOnLine = 1,
                        state = 0,
                        stateDate = DateTime.Now,
                        SteamID = "",
                        IP = guestip
                    };
                    RedisHelper.Hash_Set<LiveUserOnLine>("LiveUserOnLine" + cid.ToString(), uName, lu);
                }
                else
                {
                    query.Date = DateTime.Now;
                    RedisHelper.Hash_Remove("LiveUserOnLine" + cid.ToString(), query.UserName);
                    RedisHelper.Hash_Set<LiveUserOnLine>("LiveUserOnLine" + cid.ToString(), query.UserName, query);
                }
            }
        }

        /// <summary>
        /// 用户进入时操作redis
        /// </summary>
        public void LoginXMl2(int cid, string uName, string turename, string guestip = "")
        {
            turename = string.IsNullOrEmpty(turename) ? uName : turename;
            if (!string.IsNullOrEmpty(guestip))
            { //游客身份登录
                var list = RedisHelper.Hash_GetAll<LiveUserOnLine>("LiveUserOnLine" + cid.ToString());
                if (list == null)
                {
                    LiveUserOnLine lu = new LiveUserOnLine()
                    {
                        UserName = uName,
                        TrueName = turename,
                        Date = DateTime.Now,
                        ConnectionId = "",
                        isOnLine = 1,
                        state = 0,
                        stateDate = DateTime.Now,
                        SteamID = "",
                        IP = guestip
                    };
                    RedisHelper.Hash_Set<LiveUserOnLine>("LiveUserOnLine" + cid.ToString(), uName, lu);
                }
                else
                {
                    var gp = list.FirstOrDefault(p => p.IP == HttpContext.Current.Session.SessionID);
                    if (gp != null)
                    {
                        gp.Date = DateTime.Now;
                        gp.isOnLine = 1;
                        RedisHelper.Hash_Remove("LiveUserOnLine" + cid.ToString(), gp.UserName);
                        RedisHelper.Hash_Set<LiveUserOnLine>("LiveUserOnLine" + cid.ToString(), uName, gp);
                    }
                    else
                    {
                        LiveUserOnLine lu = new LiveUserOnLine()
                        {
                            UserName = uName,
                            TrueName = turename,
                            ConnectionId = "",
                            isOnLine = 1,
                            Date = DateTime.Now,
                            state = 0,
                            stateDate = DateTime.Now,
                            SteamID = "",
                            IP = guestip
                        };
                        RedisHelper.Hash_Set<LiveUserOnLine>("LiveUserOnLine" + cid.ToString(), uName, lu);
                    }
                }
            }
            else
            {//登录用户
                var query = RedisHelper.Hash_Get<LiveUserOnLine>("LiveUserOnLine" + cid.ToString(), uName);
                if (query == null)
                {
                    LiveUserOnLine lu = new LiveUserOnLine()
                    {
                        UserName = uName,
                        TrueName = turename,
                        Date = DateTime.Now,
                        ConnectionId = "",
                        isOnLine = 1,
                        state = 0,
                        stateDate = DateTime.Now,
                        SteamID = "",
                        IP = guestip
                    };
                    RedisHelper.Hash_Set<LiveUserOnLine>("LiveUserOnLine" + cid.ToString(), uName, lu);
                }
                else
                {
                    query.Date = DateTime.Now;
                    query.isOnLine = 1;
                    RedisHelper.Hash_Remove("LiveUserOnLine" + cid.ToString(), query.UserName);
                    RedisHelper.Hash_Set<LiveUserOnLine>("LiveUserOnLine" + cid.ToString(), query.UserName, query);
                }
            }
        }
        /// <summary>
        /// 举手操作缓存 当前举手用户 
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="userName"></param>
        /// <param name="SteamID"></param>
        public bool UserHandCatch(int cid, string userName)
        {
            var useronline = RedisHelper.Hash_Get<LiveUserOnLine>("LiveUserOnLine" + cid.ToString(), userName);
            useronline.state = 1;
            useronline.stateDate = DateTime.Now;
            RedisHelper.Hash_Remove("LiveUserOnLine" + cid.ToString(), userName);
            return RedisHelper.Hash_Set<LiveUserOnLine>("LiveUserOnLine" + cid.ToString(), userName, useronline);
        }
        /// <summary>
        /// 用户停止发言
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="userName"></param>
        /// <param name="SteamID"></param>
        /// <returns></returns>
        public LiveUserOnLine StopSpeakCatch(int cid, string userName)
        {
            var useronline = RedisHelper.Hash_Get<LiveUserOnLine>("LiveUserOnLine" + cid.ToString(), userName);
            useronline.state = 0;
            useronline.stateDate = DateTime.Now;
            RedisHelper.Hash_Remove("LiveUserOnLine" + cid.ToString(), userName);
            RedisHelper.Hash_Set<LiveUserOnLine>("LiveUserOnLine" + cid.ToString(), userName, useronline);
            return useronline;
        }
        /// <summary>
        /// 举手操作缓存 当前举手用户 
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="userName"></param>
        /// <param name="SteamID"></param>
        public LiveUserOnLine AgreeSpeakCatch(int cid, string userName, string SteamID)
        {
            var userList = RedisHelper.Hash_GetAll<LiveUserOnLine>("LiveUserOnLine" + cid.ToString()).Where(p => p.state == 2);
            foreach (var item in userList)
            {//移除现在发言者 同意其他发言者发言
                item.state = 0;
                item.stateDate = DateTime.Now;
                RedisHelper.Hash_Remove("LiveUserOnLine" + cid.ToString(), item.UserName);
                RedisHelper.Hash_Set<LiveUserOnLine>("LiveUserOnLine" + cid.ToString(), item.UserName, item);
            }
            string livestream = Edu.Tools.CombHelper.GenerateLong().ToString();
            var useronline = RedisHelper.Hash_Get<LiveUserOnLine>("LiveUserOnLine" + cid.ToString(), userName);
            useronline.state = 2;
            useronline.stateDate = DateTime.Now;
            useronline.SteamID = livestream;
            RedisHelper.Hash_Remove("LiveUserOnLine" + cid.ToString(), userName);
            RedisHelper.Hash_Set<LiveUserOnLine>("LiveUserOnLine" + cid.ToString(), userName, useronline);
            return useronline;
        }
        /// <summary>
        /// 获取当前举手或者发言用户 
        /// </summary>
        public LiveUserOnLine GetSpeakCatch(int cid)
        {
            return RedisHelper.Hash_GetAll<LiveUserOnLine>("LiveUserOnLine" + cid.ToString()).Where(p => p.state == 2).OrderByDescending(p => p.stateDate).FirstOrDefault();
        }

        /// <summary>
        /// 每隔5秒发送在线请求
        /// </summary>
        /// <returns></returns>
        public void UserOnlineSecond(int cid)
        {
            if (Edu.Service.LoginUserService.userName == "")
            {//访客的话
                var list = RedisHelper.Hash_GetAll<LiveUserOnLine>("LiveUserOnLine" + cid.ToString());
                var gp = list.FirstOrDefault(p => p.IP == HttpContext.Current.Session.SessionID);
                if (gp != null)
                {
                    gp.Date = DateTime.Now;
                    RedisHelper.Hash_Remove("LiveUserOnLine" + cid.ToString(), gp.UserName);
                    RedisHelper.Hash_Set<LiveUserOnLine>("LiveUserOnLine" + cid.ToString(), gp.UserName, gp);
                }
            }
            else
            {
                var query = RedisHelper.Hash_Get<LiveUserOnLine>("LiveUserOnLine" + cid.ToString(), Edu.Service.LoginUserService.userName);
                if (query != null)
                {
                    query.Date = DateTime.Now;
                    RedisHelper.Hash_Remove("LiveUserOnLine" + cid.ToString(), query.UserName);
                    RedisHelper.Hash_Set<LiveUserOnLine>("LiveUserOnLine" + cid.ToString(), query.UserName, query);
                }
            }
        }
    }
}
