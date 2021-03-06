using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Models
{
    /// <summary>
    /// 统一下单的商品订单信息
    /// </summary>
    public class WxPayOrderData
    {
         

        /// <summary>
        /// 商品ID, trade_type=NATIVE，此参数必传
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 商品或支付单简要描述
        /// </summary>
        public string body { get; set; }
        /// <summary>
        /// 订单总金额，单位为分
        /// </summary>
        public int mch_id { get; set; }
        /// <summary>
        /// 商品标记，代金券或立减优惠功能的参数，说明详见代金券或立减优惠
        /// </summary>
        public string nonce_str { get; set; }

        /// <summary>
        /// 交易类型,默认为:NATIVE。
        /// JSAPI--公众号支付、NATIVE--原生扫码支付、APP--app支付
        /// </summary>
        public string notify_url { get; set; }

        /// <summary>
        /// 商品名称明细列表
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// 附加数据
        /// 在查询API和支付通知中原样返回，该字段主要用于商户携带订单的自定义数据
        /// </summary>
        public string spbill_create_ip { get; set; }
        /// <summary>
        /// 用户标识
        /// trade_type=JSAPI，此参数必传，用户在商户appid下的唯一标识。
        /// </summary>
        public string time_expire { get; set; }

        public string time_start { get; set; }

        public string total_fee { get; set; }
        public string trade_type { get; set; }
      

        public WxPayOrderData()
        {
            this.trade_type = "JSAPI";
        }
    }
}
