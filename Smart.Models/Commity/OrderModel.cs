using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Models.Commity
{
   public class OrderModel
    {
        public int AdressID { get; set; }

        public string ShopCarID { get; set; }


        public string Message { get; set; }

        public decimal IntegralPrice { get; set; }

        public decimal Price { get; set; }



        public string UserID { get; set; }

        public string Des { get; set; }
        public int Count { get; set; }

        public int IsUse { get; set; }

       public string SPIP { get; set; }
       
        public int IsExpress { get; set; }

        public decimal ExpressPrice { get; set; }

        public int CommID { get; set; }

        public int ChangeCount { get; set; }

    }
}
