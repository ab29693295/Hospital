using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KNet.AAMS.Foundation.Authentication;
using KNet.AAMS.Foundation.Model;
using System.ServiceModel;
using Edu.Tools;
using System.Web;

namespace Edu.Service
{
    public class ssoUser
    {
        /// <summary>
        /// 获取单个用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public DeptUser GetUserInfo(string userName)
        {
            if (string.IsNullOrEmpty(userName)) return null;
            var fac = new ChannelFactory<IAuthenticationService>("authentication");
            try
            {
                var ser = fac.CreateChannel();
                IList<string> userNames = new List<string>();
                userNames.Add(userName);
                var users = ser.GetDeptUsersByUserNames(userNames);
                if (users == null || users.Count() == 0)
                    return null;
                return users[0];
            }
            finally
            {
                fac.Abort();
            }
        }

        public DeptUser GetUserByID(string userID)
        {
            if (string.IsNullOrEmpty(userID)) return null;
            var fac = new ChannelFactory<IAuthenticationService>("authentication");
            try
            {
                var ser = fac.CreateChannel();
                IList<string> userIDs = new List<string>();
                userIDs.Add(userID);
                var users = ser.GetDeptUsersByUserId(userID);
                
                if (users == null || users.Count() == 0)
                    return null;
                return users[0];
            }
            finally
            {
                fac.Abort();
            }
        }
        public string GetHead(string uname = "")
        {
            if (uname == "")
            {
                uname = Edu.Service.LoginUserService.userName;

            }
            string imghead = HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/Content/images/author.jpg";
            try
            {
                var query = this.GetUserInfo(uname);


                if (ConfigHelper.GetConfigString("IsSso") == "1")
                {
                    imghead = KNet.AAMS.Web.Security.AuthenticationConstant.GetUserLogoUrl(query.UserId);
                }
                else
                {
                    var user = new UnitOfWork().DUserInfo.GetByID(uname);
                    imghead = user.Phone==null? imghead : user.Phone;
                }
                return imghead;
            }
            catch
            {
                return imghead;
            }

        }

    }
}
