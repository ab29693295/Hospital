using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using Edu.Tools;
using Smart.Data;
using Smart.Entity;
using Smart.Service;
using System.Web.Security;

namespace Smart.Service
{
    public class LoginUserService
    {
        public static string userName
        {
            get
            {

                string cookieName = FormsAuthentication.FormsCookieName;
                HttpCookie authCookie = HttpContext.Current.Request.Cookies[cookieName];
                if (authCookie != null && authCookie.Value != "")
                {
                    FormsAuthenticationTicket authTicket = null;
                    authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    return authTicket.Name;
                }
                else return "";

            }
        }
        public static Smart.Entity.User User
        {
            get
            {
                if (userName == "")
                {
                    return null;
                }
                Smart.Entity.User user;
                user = new UnitOfWork().DUser.Get(p=>p.UserName==userName).FirstOrDefault();
                return user;
            }
        }
    }
}
