using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Models
{
    public class AddressModel
    {

        public int ID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 用户真实姓名
        /// </summary>
        public string TrueName { get; set; }

       /// <summary>
       /// 用户联系电话
       /// </summary>
        public string Phone { get; set; }
        
        /// <summary>
        /// 快递地址
        /// </summary>
        public string Adress { get; set; }
        /// <summary>
        /// 是否默认是收货地址
        /// </summary>
        public int? IsDefult { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Des { get; set; }

    }
}
