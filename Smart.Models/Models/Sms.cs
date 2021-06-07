using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Models
{
    public class Sms
    {
        /// <summary>
        /// 接收人手机号码
        /// </summary>
        public string Receiver { get; set; }
        /// <summary>
        /// 短信内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 接收人姓名(可为空)
        /// </summary>
        public string ReceiveMan { get; set; }

    }
}
