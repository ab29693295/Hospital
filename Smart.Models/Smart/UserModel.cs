using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Models
{
  public  class UserModel
    {
    
       /// <summary>
       /// 用户唯一ID
       /// </summary>
        public string UserID { get; set; }
      
        public string UserName { get; set; }

        public string TrueName { get; set; }


        public string Password { get; set; }
       /// <summary>
       /// 用户昵称
       /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string HeadImage { get; set; }
    }
}
