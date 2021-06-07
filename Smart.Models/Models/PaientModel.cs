using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Models.Models
{
    public class PaientModel
    {
        public int ID { get; set; }

    
        public string username { get; set; }

       
        public string sex { get; set; }

        public int? age { get; set; }

        
        public string hospNumber { get; set; }

        public DateTime createtime { get; set; }
      
        public string operaName { get; set; }
       
        public string operativesite { get; set; }

      
        public string surgeon { get; set; }
       
        public string Department { get; set; }

        public DateTime operationDate { get; set; }

        public int? status { get; set; }

      
        public string des { get; set; }

        public bool IsCheck { get; set; }
    }
}
