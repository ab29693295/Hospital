using Edu.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Models
{
    /// <summary>
    /// 审核状态
    /// </summary>
    public enum EnumState
    {
        [EnumDescription("未通过")]
        No = -1,
        [EnumDescription("等待审核")]
        Initial = 0,
        [EnumDescription("审核通过")]
        Yes = 1
    }
    /// <summary>
    /// 审核状态
    /// </summary>
    public enum EnumStateColor
    {
        [EnumDescription("<span style='color:red'> 未通过</span>")]
        No = -1,
        [EnumDescription("等待审核")]
        Initial = 0,
        [EnumDescription("<span style='color:green'>审核通过</sapn>")]
        Yes = 1
    }

}
