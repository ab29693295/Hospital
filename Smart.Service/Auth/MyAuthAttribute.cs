using Smart.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Smart.Service
{
    /// <summary>
    /// 权限认证 如果多个角色用逗号分隔
    /// </summary>
    public class MyAuthAttribute : System.Web.Mvc.AuthorizeAttribute
    {

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (LoginUserService.userName == null || LoginUserService.userName == "")
            {
              
                    return false;
               
               
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 是否是管理员 默认否
        /// </summary>
        public string isAdmin = "0";

    }
}
