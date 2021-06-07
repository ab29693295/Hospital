using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

using System.Reflection;
using System.Runtime.Serialization.Json;

using System.Web.Script.Serialization;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Smart.Models
{
    public class JSONHelper
    {

        public static string ObjectToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static string ObjToJson<T>(T data)
        {
            try
            {
                System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(data.GetType());
                using (MemoryStream ms = new MemoryStream())
                {
                    serializer.WriteObject(ms, data);
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            catch
            {
                return null;
            }
        }

        public static T Deserialize<T>(string url)
        {
            
            string json = JSONHelper.Get(url);
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            //T alive = (T)Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            T obj = serializer.Deserialize<T>(json);
            return obj;
        }

        public static string Get(string Url)
        {
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
           // Edu.Tools.LogHelper.Info("retString"+ retString);
            return retString;
        }

        /// <summary> 
        /// JSON序列化
        /// 将泛型list对象转换成JSON类型串
        /// <param name="t">对象（实体对象或其他对象）</param>
        /// </summary> 
        public static string JsonSerializer_to_Json<T>(T t)
        {

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));

            MemoryStream ms = new MemoryStream();

            ser.WriteObject(ms, t);

            string jsonString = Encoding.UTF8.GetString(ms.ToArray());

            ms.Close();

            //替换Json的Date字符串 

            string p = @"\\/Date(\d+)\+\d+\\/";

            MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertJsonDateToDateString);

            Regex reg = new Regex(p);

            jsonString = reg.Replace(jsonString, matchEvaluator);

            return jsonString;

        }
        /// <summary> 
        /// JSON反序列化 
        /// 将JSON类型串转换成泛型list对象
        /// <param name="jsonString">json格式的字符串</param>
        /// </summary> 
        public static T JsonDeserialize_JsonTo<T>(string jsonString)
        {
            //将"yyyy-MM-dd HH:mm:ss"格式的字符串转为"\/Date(1294499956278+0800)\/"格式 

            string p = @"\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}";

            MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertDateStringToJsonDate);

            Regex reg = new Regex(p);

            jsonString = reg.Replace(jsonString, matchEvaluator);

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));

            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));

            T obj = (T)ser.ReadObject(ms);

            return obj;
        }

        /// <summary> 
        /// 将Json序列化的时间由/Date(1294499956278+0800)转为字符串 
        /// </summary> 
        private static string ConvertJsonDateToDateString(Match m)

        {

            string result = string.Empty;

            DateTime dt = new DateTime(1970, 1, 1);

            dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));

            dt = dt.ToLocalTime();

            result = dt.ToString("yyyy-MM-dd HH:mm:ss");

            return result;

        }
        /// <summary> 
        /// 将时间字符串转为Json时间 
        /// </summary> 
        private static string ConvertDateStringToJsonDate(Match m)

        {

            string result = string.Empty;

            DateTime dt = DateTime.Parse(m.Groups[0].Value);

            dt = dt.ToUniversalTime();

            TimeSpan ts = dt - DateTime.Parse("1970-01-01");

            result = string.Format("\\/Date({0}+0800)\\/", ts.TotalMilliseconds);

            return result;

        }
        /// <summary>
        ///  将泛型list对象转换成JSON类型串（二）
        /// </summary>
        /// <typeparam name="T">对象类型或实体类型</typeparam>
        /// <param name="jsonName">对该对象起名字</param>
        /// <param name="IL">Ilist对象</param>
        /// <returns></returns>
        public static string ObjectToJson<T>(string jsonName, IList<T> IL)
        {
            StringBuilder Json = new StringBuilder();
            Json.Append("{\"" + jsonName + "\":[");
            if (IL.Count > 0)
            {
                for (int i = 0; i < IL.Count; i++)
                {
                    T obj = Activator.CreateInstance<T>();
                    Type type = obj.GetType();
                    PropertyInfo[] pis = type.GetProperties();
                    Json.Append("{");
                    for (int j = 0; j < pis.Length; j++)
                    {
                        Json.Append("\"" + pis[j].Name.ToString() + "\":\"" + pis[j].GetValue(IL[i], null) + "\"");
                        if (j < pis.Length - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < IL.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]}");
            return Json.ToString();
        }

        /// <summary>
        /// 将数据表转换成JSON类型串
        /// </summary>
        /// <param name="dt">要转换的数据表</param>
        /// <returns></returns>
        public static StringBuilder DataTableToJSON(System.Data.DataTable dt)
        {
            return DataTableToJSON(dt, true);
        }

        /// <summary>
        /// 将数据表转换成JSON类型串
        /// </summary>
        /// <param name="dt">要转换的数据表</param>
        /// <param name="dispose">数据表转换结束后是否dispose掉</param>
        /// <returns></returns>
        public static StringBuilder DataTableToJSON(System.Data.DataTable dt, bool dt_dispose)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("[");

            //数据表字段名和类型数组
            string[] dt_field = new string[dt.Columns.Count];
            int i = 0;
            string formatStr = "{{";
            string fieldtype = "";
            foreach (System.Data.DataColumn dc in dt.Columns)
            {
                dt_field[i] = dc.Caption.ToLower().Trim();
                formatStr += '"' + dc.Caption.ToLower().Trim() + '"' + ":";
                fieldtype = dc.DataType.ToString().Trim().ToLower();
                if (fieldtype.IndexOf("int") > 0 || fieldtype.IndexOf("deci") > 0 ||
                    fieldtype.IndexOf("floa") > 0 || fieldtype.IndexOf("doub") > 0 ||
                    fieldtype.IndexOf("bool") > 0)
                {
                    formatStr += "{" + i + "}";
                }
                else
                {
                    formatStr += '"' + "{" + i + "}" + '"';
                }
                formatStr += ",";
                i++;
            }

            if (formatStr.EndsWith(","))
            {
                formatStr = formatStr.Substring(0, formatStr.Length - 1);//去掉尾部","号
            }
            formatStr += "}},";

            i = 0;
            object[] objectArray = new object[dt_field.Length];
            foreach (System.Data.DataRow dr in dt.Rows)
            {

                foreach (string fieldname in dt_field)
                {   //对 \ , ' 符号进行转换
                    objectArray[i] = dr[dt_field[i]].ToString().Trim().Replace("\\", "\\\\").Replace("'", "\\'").Replace("\n", "");
                    switch (objectArray[i].ToString())
                    {
                        case "True":
                            {
                                objectArray[i] = "true"; break;
                            }
                        case "False":
                            {
                                objectArray[i] = "false"; break;
                            }
                        default: break;
                    }
                    i++;
                }
                i = 0;
                stringBuilder.Append(string.Format(formatStr, objectArray));
            }
            if (stringBuilder.ToString().EndsWith(","))
            {
                stringBuilder.Remove(stringBuilder.Length - 1, 1);//去掉尾部","号
            }

            if (dt_dispose)
            {
                dt.Dispose();
            }
            return stringBuilder.Append("]");
        }

        /// <summary>
        /// datatable分页
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public static DataTable GetPagedTable(DataTable dt, int PageIndex, int PageSize)
        {

            if (PageIndex == 0)
                return dt;
            DataTable newdt = dt.Clone();

            int rowbegin = (PageIndex - 1) * PageSize;
            int rowend = PageIndex * PageSize;

            if (rowbegin >= dt.Rows.Count)
                return newdt;

            if (rowend > dt.Rows.Count)
                rowend = dt.Rows.Count;
            for (int i = rowbegin; i <= rowend - 1; i++)
            {
                DataRow newdr = newdt.NewRow();
                DataRow dr = dt.Rows[i];
                foreach (DataColumn column in dt.Columns)
                {
                    newdr[column.ColumnName] = dr[column.ColumnName];
                }
                newdt.Rows.Add(newdr);
            }

            return newdt;
        }
        /// <summary>
        /// 返回有效名称
        /// </summary>
        /// <param name="unName"></param>
        /// <param name="unEnName"></param>
        /// <returns></returns>
        public static string GetValidateName(string firName, string secEnName)
        {
            string strName = "";
            if (!String.IsNullOrEmpty(firName))
            {
                strName = firName;
            }
            else
            {
                if (!String.IsNullOrEmpty(secEnName))
                {
                    strName = secEnName;
                }
            }
            return strName;
        }

    }
}