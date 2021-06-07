using Edu.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Smart.Models
{
    public class AccessToken
    {

        public static string AppID =ConfigHelper.GetConfigString("AppID");

        public static string AppPwd= ConfigHelper.GetConfigString("AppPwd");
        public static string Get(string code)
        {
            string Url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid="+ AppID + "&secret="+ AppPwd+ "&code="+code+ "&grant_type = authorization_code";
            //System.GC.Collect();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Proxy = null;
            request.KeepAlive = false;
            request.Method = "GET";
            request.ContentType = "application/json; charset=UTF-8";
            request.AutomaticDecompression = DecompressionMethods.GZip;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();

            myStreamReader.Close();
            myResponseStream.Close();

            if (response != null)
            {
                response.Close();
            }
            if (request != null)
            {
                request.Abort();
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            Token obj = serializer.Deserialize<Token>(retString);

            string a = obj.access_token;

            return obj.access_token;
        }
        /// <summary>
        /// 根据第三方权限的Code获取Token
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static CodeToken GetOpenIDToken(string code)
        {
            try
            {
                string url = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + AppID + "&secret=" + AppPwd + "&code=" + code + "&grant_type=authorization_code";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Proxy = null;
                request.KeepAlive = false;
                request.Method = "GET";
                request.ContentType = "application/json; charset=UTF-8";
                request.AutomaticDecompression = DecompressionMethods.GZip;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                string retString = myStreamReader.ReadToEnd();

                myStreamReader.Close();
                myResponseStream.Close();

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                CodeToken obj = serializer.Deserialize<CodeToken>(retString);

                string a = obj.access_token;

                return obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
