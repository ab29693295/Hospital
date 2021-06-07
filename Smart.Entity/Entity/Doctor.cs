namespace Smart.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Doctor")]
    public partial class Doctor
    {
        public int ID { get; set; }

        [StringLength(255)]
        public string UserID { get; set; }

        [StringLength(255)]
        public string username { get; set; }

        public int? sex { get; set; }

        public int? age { get; set; }

        public int? categoryID { get; set; }

        [StringLength(255)]
        public string categoryName { get; set; }

        public DateTime? createtime { get; set; }

        public int? status { get; set; }

        [Column(TypeName = "text")]
        public string des { get; set; }
    }
}
