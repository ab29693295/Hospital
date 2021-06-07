using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    public class PainetMaster
    {
        public string UserName { get; set; }
        public string Sex { get; set; }
        public string Title { get; set; }
        public string Age { get; set; }
        public string Phone { get; set; }
        public string Nei { get; set; }
        public string Hos { get; set; }
        public string Des { get; set; }
        public string Categy { get; set; }
        public string Address { get; set; }
     
     
        public List<ImageModel> OrderDetails = new List<ImageModel>();

       
    }

    public class PainetDetail
    {
        public string Sku { get; set; }
        public string Spec { get; set; }
        public decimal Number { get; set; }
        public string Unit { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
    }
}
