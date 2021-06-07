using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Models
{
    public class WxPayData
    {
        //采用排序的Dictionary的好处是方便对数据包进行签名，不用再签名之前再做一次排序
        private SortedDictionary<string, object> m_values = new SortedDictionary<string, object>();

        /// <summary>
        /// 默认构造函数
        /// 如果initDefault为true，则自动填入字段（appid,mch_id,time_stamp,nonce_str,out_trade_no,）
        /// </summary>
        public WxPayData(bool initDefault = false)
        {
            if (initDefault)
            {
                Init();
            }
        }

        /// <summary>
        /// 对象初始化后，自动填入字段（appid,mch_id,time_stamp,nonce_str,out_trade_no,）
        /// </summary>
        public void Init()
        {
          
        }

    }
}
