using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Models
{
    public class DiscussModel
    {
        public int ID { get; set; }

      
        public string UserName { get; set; }


        public string ContentText { get; set; }

        public string CreateTime { get; set; }


        public int? MessageID { get; set; }

        public int? CationID { get; set; }


        public string UserID { get; set; }

        public int? Cell { get; set; }


        public List<string> ImageList { get; set; }
    }
}
