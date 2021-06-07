namespace Smart.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Patient")]
    public partial class Patient
    {
        public int ID { get; set; }

        [StringLength(255)]
        public string username { get; set; }
       
        [StringLength(255)]
        public string sex { get; set; }
      
        public int? age { get; set; }

        [StringLength(255)]
        public string hospNumber { get; set; }

        public DateTime createtime { get; set; }
        [StringLength(255)]
        public string operaName { get; set; }
        [StringLength(255)]
        public string operativesite { get; set; }

        [StringLength(255)]
        public string surgeon { get; set; }
        [StringLength(255)]
        public string Department { get; set; }
        
        public DateTime operationDate { get; set; }

        public int? status { get; set; }

        [Column(TypeName = "text")]
        public string des { get; set; }

    }
}
