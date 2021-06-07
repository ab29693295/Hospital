using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Models
{
   public  class CommType
    {
        public int ID { get; set; }


        public string ToID { get; set; }
     
        public string CommTypeName { get; set; }

        public int Orderby { get; set; }

   
        public string FatherName { get; set; }

        public int? FatherID { get; set; }

   
        public string Des { get; set; }
      
        public string ImagePath { get; set; }
        public int Cell { get; set; }


        public List<CommType> CommTypeList { get; set; }
    }
}
