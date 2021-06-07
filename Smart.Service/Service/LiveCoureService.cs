using Edu.Entity;
using Edu.Models;
using Edu.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ThoughtWorks.QRCode.Codec;

namespace Edu.Service
{
    public class LiveCoureService: CoreServiceBase
    {
        /// <summary>
        /// 获取当前课程加入缓存
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public LiveCourse GetCurLiveCourse(int cid)
        {
            var old = Edu.Tools.CacheHelper.GetCache("PLiveLiveCourse" + cid);
            if (old == null)
            {
                var host = unitOfWork.DLiveCourse.GetByID(cid);

                Edu.Tools.CacheHelper.SetCache("PLiveLiveCourse" + cid, host);
                return host;
            }
            else
            {
                return old as LiveCourse;
            }
        }

        public UserInfo GetHostUser(int cid)
        {
            var old = Edu.Tools.CacheHelper.GetCache("PLiveLiveCourseHost" + cid);
            if (old == null)
            {
                var host = this.GetCurLiveCourse(cid);
                var userName = host.Creator;
                var user = unitOfWork.DUserInfo.GetByID(userName);
                Edu.Tools.CacheHelper.SetCache("PLiveLiveCourseHost" + cid, user);
                return user;
            }
            else
            {
                return old as UserInfo;
            }
        }
        /// <summary>
        /// redis 记录播放方式
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>

        public string loadBrocastType(int cid)
        {
            var bct = RedisHelper.Hash_Get<string>("Livebroadcast", cid.ToString());
            if (bct == null)
            {
                return "1";
            }
            return bct;
        }
        public void setBrocastType(int cid, string _vaule)
        {

            RedisHelper.Hash_Set<string>("Livebroadcast" , cid.ToString(), _vaule);
        }

        public bool GeneQRImage(int id)
        {
            var resource = unitOfWork.DLiveCourse.GetByID(id);
            string data = ConfigHelper.GetConfigString("website").TrimEnd('/') + "/LiveApp/Index?cid=" + id;
            string uploadsFolder = HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/File/QRImage/";//附件路径文件
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(uploadsFolder)))
            {
                Edu.Tools.FileHelper.CreateDirectory(HttpContext.Current.Server.MapPath(uploadsFolder));
            }
            Guid identifier = Guid.NewGuid();
            var uploadsPath = HttpContext.Current.Server.MapPath(uploadsFolder) + "\\" + identifier.ToString() + ".jpg";
            if (File.Exists(uploadsPath))
            {//存在文件
                return true;
            }
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeScale = 4;
            qrCodeEncoder.QRCodeVersion = 0;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            Image image = qrCodeEncoder.Encode(data, Encoding.UTF8);
            image.Save(uploadsPath, ImageFormat.Jpeg);
            resource.QImage = uploadsFolder + identifier.ToString() + ".jpg";
            unitOfWork.DLiveCourse.Update(resource);
            var result = unitOfWork.SaveRMsg();
            if (result.ToLower() == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
       

    }
}
