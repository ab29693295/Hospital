using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edu.Tools;
namespace Smart.Models
{
    public enum UserInfo_isAdmin
    {

        [EnumDescription("管理用户")]
        admin = 1,
        [EnumDescription("普通用户")]
        commonUser = 2
    }
    public enum UserInfo_UserState
    {
        [EnumDescription("被锁定")]
        Lock = 0,
        [EnumDescription("正常登陆")]
        Normal = 1
    }
    public enum UserInfo_Role
    {
        [EnumDescription("超级管理员")]
        admin = 1,
        [EnumDescription("教师")]
        teacher = 2,
        [EnumDescription("普通用户")]
        User = 3

    }

}
