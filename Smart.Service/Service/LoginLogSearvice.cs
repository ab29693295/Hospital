using Edu.Models;
using Edu.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edu.Service
{
   public  class LoginLogSearvice
    {
        public bool insertLog(string userName, string unitCode, string Phone )
        {

            string oID = CombHelper.GenerateNumber();
            LoginLog log = new LoginLog();
            log.UserName = userName;
            log.UnitCode = unitCode;
            log.Phone = Phone;  
            log.CreateDate = DateTime.Now;
            var result = RedisHelper.Hash_Set<LoginLog>("LoginLog", oID, log);
            return result;
        }

        public List<LoginLog> GetAllLoginLog(string sn = "")
        {
            var list = RedisHelper.Hash_GetAll<LoginLog>("LoginLog").ToList();
           
            if (list != null)
            {
                
                if (sn != "")
                {
                    list = list.Where(p => p.Phone.Contains(sn) || p.UserName.Contains(sn) || p.UnitCode.Contains(sn)).ToList();
                }
                return list.OrderBy(p => p.CreateDate).ToList();
            }
            else
            {
                list = new List<LoginLog>();
            }
            return list;
        }
    }
}
