using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
   public class ImageModel
    {
        public int ID { get; set; }

      
        public string name { get; set; }

        public int? operationID { get; set; }

    
        public string doctor { get; set; }

        public string des { get; set; }

        public bool IsSelected { get; set; }
        public string imagePath { get; set; }
    }

    public class VideoeModel
    {
        public int ID { get; set; }


        public string name { get; set; }

        public int? operationID { get; set; }
        
        public string duration { get; set; }

        public string doctor { get; set; }

        public string des { get; set; }

        public bool IsSelected { get; set; }
        public string videoPath { get; set; }
    }
}
