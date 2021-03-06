using Edu.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Models
{
    /// <summary>
    /// 日志类型
    /// </summary>
    public enum YesNo
    {

        [EnumDescription("否")]
        No = 0,
        [EnumDescription("是")]
        Yes = 1
    }
    /// <summary>
    /// 日志类型
    /// </summary>
    public enum YesNoColor
    {

        [EnumDescription("<span style='color:red'>否</span>")]
        No = 0,
        [EnumDescription("<span style='color:blue'>是</span>")]
        Yes = 1
    }
    /// <summary>
    /// 日志类型
    /// </summary>
    public enum YesNoSpeak
    {

        [EnumDescription("<span style='color:red'>是</span>")]
        No = 0,
        [EnumDescription("<span style='color:blue'>否</span>")]
        Yes = 1
    }
}
