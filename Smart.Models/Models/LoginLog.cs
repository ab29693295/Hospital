using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Models
{
   public class LoginLog
    {
        public string UserName { get; set; }
        public string Phone { get; set; }

        public string UnitCode { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
