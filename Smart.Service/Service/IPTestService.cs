using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Smart.Service
{
   public class IPTestService
    {
        public static string GetWebClientIp()
        {

            string userIP = "NULL";

            try
            {
                if (HttpContext.Current == null
                 || HttpContext.Current.Request == null
                 || HttpContext.Current.Request.ServerVariables == null)
                {
                    return "";
                }

                string CustomerIP = "";

                //CDN加速后取到的IP simone 090805
                CustomerIP = HttpContext.Current.Request.Headers["Cdn-Src-Ip"];
                if (!string.IsNullOrEmpty(CustomerIP))
                {
                    return CustomerIP;
                }

                CustomerIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (!String.IsNullOrEmpty(CustomerIP))
                {
                    return CustomerIP;
                }

                if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                {
                    CustomerIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                    if (CustomerIP == null)
                    {
                        CustomerIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    }
                }
                else
                {
                    CustomerIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }

                if (string.Compare(CustomerIP, "unknown", true) == 0 || String.IsNullOrEmpty(CustomerIP))
                {
                    return HttpContext.Current.Request.UserHostAddress;
                }

                //if (CustomerIP == "::1") { CustomerIP = "127.0.0.1"; }
                return CustomerIP;
            }
            catch { }

            return userIP;

        }


        public static string GetIP()
        {
            string loginip = "";
            //Request.ServerVariables[""]--获取服务变量集合   
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"])) //判断发出请求的远程主机的ip地址是否为空  
            {
                //获取发出请求的远程主机的Ip地址  
                loginip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            //判断登记用户是否使用设置代理  
            else if (!string.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTP_VIA"]))
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]))
                {
                    //获取代理的服务器Ip地址  
                    loginip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                }
                else
                {
                    //获取客户端IP  
                    loginip = HttpContext.Current.Request.UserHostAddress;
                }
            }
            else
            {
                //获取客户端IP  
                loginip = HttpContext.Current.Request.UserHostAddress;
            }


            if (loginip.ToLower().Equals("::1"))
            {
                loginip = "127.0.0.1";
            }
            return loginip;
        }

    }
}
