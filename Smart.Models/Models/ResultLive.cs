using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Models
{
    public class ResultLive<T>
    {
        public string R { get; set; }
        public IEnumerable<T> Data { get; set; }
        public string M { get; set; }

        public string TrueName { get; set; }
     
        public string HeadPhoto { get; set; }

    }
}
