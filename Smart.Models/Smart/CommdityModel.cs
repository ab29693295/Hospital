using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Models
{
    public  class CommdityModel
    {
        public int ID { get; set; }

      
        public string Name { get; set; }

        public int? ShopID { get; set; }

      
        public string ShopName { get; set; }

        public decimal Price { get; set; }

        public int? Integral { get; set; }

    
        public string ComDetail { get; set; }

        
        public string ComDes { get; set; }

        public int? IsTop { get; set; }

        public int? IsRecommand { get; set; }

        public int? CationID { get; set; }

       
        public string CationName { get; set; }

     
        public string Creator { get; set; }

        public int? SaleCount { get; set; }

        public int? StoreCount { get; set; }

        public int? SurCount { get; set; }


        public decimal? SalePrice { get; set; }


        public int? IsSale { get; set; }


     
        public string Tags { get; set; }

        public int? CommTypeID { get; set; }

      
        public string CommTypeName { get; set; }


        public string Image { get; set; }

        public List<string> ImageList { get; set; }


        public int? IsChange { get; set; }

        public int? ChangeCount { get; set; }
    }
}
