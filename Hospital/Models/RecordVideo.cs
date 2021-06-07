using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    public class RecordVideo
    {
        public int ID { get; set; }

        public int? PatintUserID { get; set; }
     
        public string username { get; set; }
    
        public string sex { get; set; }

        public int? age { get; set; }

        public int? categoryID { get; set; }

        public string categoryName { get; set; }

    
        public string hospNumber { get; set; }
     
        public string operaName { get; set; }
   
        public string operativesite { get; set; }

   
        public string surgeon { get; set; }

        public DateTime? createtime { get; set; }

        public string CreateDate { get; set; }

        public int? status { get; set; }

        public int VideoCount { get; set; }

        public int ImageCount { get; set; }

        public string des { get; set; }
    }

    public class OperationName
    {
        public string Name { get; set; }
    }
}
