using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hospital
{
    public static class XmlHelper
    {
        private static readonly string XmlPath = System.Windows.Forms.Application.StartupPath + "\\CheckAPP.config";//网站配置xml
        /// <summary>
        /// 设置对应选项的Index和Value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public static void SetValue(string key, int index, string value)
        {
            XElement root = XElement.Load(XmlPath);
            var old =
                root.Elements("Metas").Elements("Meta").Where(p => p.Attribute("ID").Value == key).FirstOrDefault();
            if (old != null)
            {
                old.SetAttributeValue("Name", index);
                old.SetValue(value);
                root.Save(XmlPath);
            }
            else
            {
                XElement ele = new XElement(
                    "Meta",
                    new XAttribute("ID", key),
                    new XAttribute("Name", index),
                    value);
                root.Element("Metas").Add(ele);
                root.Save(XmlPath);
            }
        }
       
        /// <summary>
        /// 设置对应选项的Value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetValue(string key, string value)
        {
            XElement root = XElement.Load(XmlPath);
            var old =
                root.Elements("Metas").Elements("Meta").Where(p => p.Attribute("ID").Value == key).FirstOrDefault();
            if (old != null)
            {
                old.SetAttributeValue("Name", 0);
                old.SetValue(value);
                root.Save(XmlPath);
            }
            else
            {
                XElement ele = new XElement(
                    "Meta",
                    new XAttribute("ID", key),
                    new XAttribute("Name", 0),
                    value);
                root.Element("Metas").Add(ele);
                root.Save(XmlPath);
            }
        }


        /// <summary>
        /// 获取对应选项的文本值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetValue(string key)
        {
            XElement root = XElement.Load(XmlPath);
            var old = root.Elements("Metas").Elements("Meta").Where(p => p.Attribute("ID").Value == key).FirstOrDefault();
            if (old != null)
            {
                try
                {
                    return old.Value;
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 获取对应选项的Index值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetIndexValue(string key)
        {
            XElement root = XElement.Load(XmlPath);
            var old = root.Elements("Metas").Elements("Meta").Where(p => p.Attribute("ID").Value == key).FirstOrDefault();
            if (old != null)
            {
                try
                {
                    return Convert.ToInt32(old.Attribute("Name").Value);
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }


        /// <summary>
        /// 获取ID和Name都符合的节点的 Text
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Single GetValue(string id, string name)
        {
            XElement root = XElement.Load(XmlPath);
            var old = root.Elements("Metas").Elements("Meta").Where(
                p => p.Attribute("ID").Value == id && p.Attribute("Name").Value == name).FirstOrDefault();
            if (old != null)
            {
                try
                {
                    return Convert.ToSingle(old.Value);
                }
                catch (Exception ex)
                {
                    return 1;
                }
            }
            else
            {
                return 1;
            }
        }


        /// <summary>
        /// 获取本地存储的所有用户名
        /// </summary>
        /// <returns></returns>
        public static List<string> GetStoreUsers()
        {
            var users=new List<string>();
            XElement root = XElement.Load(XmlPath);
            var old = root.Elements("Users");
            if (old.Elements().Count()!=0)
            {
                foreach (var ele in old.Elements())
                {
                    if (ele.Attribute("UserName").Value != "")
                    {
                        users.Add(ele.Attribute("UserName").Value);
                    }
                }
            }
            return users;
        }

        /// <summary>
        /// 获取手术列表名称
        /// </summary>
        /// <returns></returns>
        public static List<string> GetOperations()
        {
            var users = new List<string>();
            XElement root = XElement.Load(XmlPath);
            var old = root.Elements("Operations");
            if (old.Elements().Count() != 0)
            {
                foreach (var ele in old.Elements())
                {
                    if (ele.Attribute("OperationName").Value != "")
                    {
                        users.Add(ele.Attribute("OperationName").Value);
                    }
                }
            }
            return users;
        }
        /// <summary>
        /// 设置医生名称
        /// </summary>
        public static void SetUsers(string userName)
        {
            XElement root = XElement.Load(XmlPath);
            var old = root.Elements("Users").Elements("User").Where(p => p.Attribute("UserName").Value == userName).FirstOrDefault();
            if (old != null)
            {

              
            }
            else
            {
                XElement ele = new XElement("User", new XAttribute("UserName", userName));
                root.Element("Users").AddFirst(ele);
                root.Save(XmlPath);
            }
        }

        /// <summary>
        /// 设置手术名称
        /// </summary>
        public static void StoreOperations(string userName)
        {
            XElement root = XElement.Load(XmlPath);
            var old = root.Elements("Operations").Elements("Operation").Where(p => p.Attribute("OperationName").Value == userName).FirstOrDefault();
            if (old != null)
            {
               
              
            }
            else
            {
                XElement ele = new XElement("Operation", new XAttribute("OperationName", userName));
                root.Element("Operations").AddFirst(ele);
                root.Save(XmlPath);
            }
        }
        /// <summary>
        /// 设置手术部位
        /// </summary>
        public static void StoreOperaPart(string userName)
        {
            XElement root = XElement.Load(XmlPath);
            var old = root.Elements("OperationParts").Elements("Part").Where(p => p.Attribute("OperationName").Value == userName).FirstOrDefault();
            if (old != null)
            {

            
            }
            else
            {
                XElement ele = new XElement("Part", new XAttribute("OperationName", userName));
                root.Element("OperationParts").AddFirst(ele);
                root.Save(XmlPath);
            }
        }
        /// <summary>
        /// 设置部门名称
        /// </summary>
        public static void StoreDepartments(string userName)
        {
            XElement root = XElement.Load(XmlPath);
            var old = root.Elements("Departments").Elements("Part").Where(p => p.Attribute("Department").Value == userName).FirstOrDefault();
            if (old != null)
            {

            }
            else
            {
                XElement ele = new XElement("Part", new XAttribute("Department", userName));
                root.Element("Departments").Add(ele);
                root.Save(XmlPath);
            }
        }

        /// <summary>
        /// 获取手术部位名称
        /// </summary>
        /// <returns></returns>
        public static List<string> GetOperationParts()
        {
            var users = new List<string>();
            XElement root = XElement.Load(XmlPath);
            var old = root.Elements("OperationParts");
            if (old.Elements().Count() != 0)
            {
                foreach (var ele in old.Elements())
                {
                    if (ele.Attribute("OperationName").Value != "")
                    {
                        users.Add(ele.Attribute("OperationName").Value);
                    }
                 
                }
            }
            return users;
        }
        /// <summary>
        /// 获取科室列表
        /// </summary>
        /// <returns></returns>
        public static List<string> GetDepartments()
        {
            var users = new List<string>();
            XElement root = XElement.Load(XmlPath);
            var old = root.Elements("Departments");
            if (old.Elements().Count() != 0)
            {
                foreach (var ele in old.Elements())
                {
                    users.Add(ele.Attribute("Department").Value);
                }
            }
            return users;
        }
        /// <summary>
        /// 获取当前存储的第一个用户的用户名和密码，然后自动显示在文本框中
        /// </summary>
        /// <returns></returns>
        public static string GetFirstStoreUser()
        {

            var users = new List<string>();
            XElement root = XElement.Load(XmlPath);
            var old = root.Elements("Users");
            if (old.Elements().Count() != 0)
            {
                return old.ElementAt(0).Element("User").Attribute("UserName").Value;
            }
            return "";
        }

        /// <summary>
        /// 将用户名和密码保存到本地
        /// </summary>
        public static void StoreUsers(string userName,string pwd)
        {
            XElement root = XElement.Load(XmlPath);
            var old =
                root.Elements("Users").Elements("User").Where(p => p.Attribute("UserName").Value == userName).FirstOrDefault();
            if (old != null)
            {
                old.SetValue(pwd);
                root.Save(XmlPath);
            }
            else
            {
                XElement ele = new XElement(
                    "User",
                    new XAttribute("UserName", userName),
                    pwd);
                root.Element("Users").Add(ele);
                root.Save(XmlPath);
            }
        }

        /// <summary>
        /// 获取对应用户名的密码
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static string GetUserPassword(string userName)
        {
            XElement root = XElement.Load(XmlPath);
            var old = root.Elements("Users").Elements("User").Where(p => p.Attribute("UserName").Value == userName).FirstOrDefault();
            if (old != null)
            {
                try
                {
                    return old.Value;
                }
                catch (Exception ex)
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
    }

   
}
