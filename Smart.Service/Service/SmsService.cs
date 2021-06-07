using Edu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edu.Service.Service
{
    public class SmsService
    {
        /// <summary>
        /// 添加短信发送信息（通用）函数：SMS_Add_ForAll　
        /// </summary>
        /// <param name="Receiver">接收人手机号码</param>
        /// <param name="Content">短信内容</param>
        /// <param name="ReceiveMan">接收人姓名(可为空)</param>
        /// <param name="Sender">发送人姓名(可为空，建议填写调用者姓名</param>
        /// <param name="DepNum">发送人所属部门编码(联系管理员提供) 1010340111203</param>
        /// <param name="DepName">发送人所属部门名称(联系管理员提供) 知识管理产品研发中心 </param>
        /// <param name="MessType">短信内容类别(自类义类别，可为空)</param>
        /// <param name="ServiceName">短信服务类别(联系管理员提供 '') </param>
        /// <param name="NeedTime">短信发送时间(空表示直接发送，不等待)</param>
        /// <param name="IsPush">是否PUSH(true/false)</param>
        /// <returns>＞0:发送成功，返回值为标识ID；-1:IP不在范围；-100:接收人手机号码错误；</returns>
        public static int SMS_Add_ForAll(Sms sms)
        {
            SMS.SMSSoapClient cc = new SMS.SMSSoapClient();
            var returnInt = cc.SMS_Add_ForAll(sms.Receiver, sms.Content, sms.ReceiveMan, "于江虎", "1010340111203", "知识管理产品研发中心", "", "未来期刊", "", true);
            return returnInt;
        }
    }
}
