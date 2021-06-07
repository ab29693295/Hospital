namespace Smart.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Department")]
    public partial class Department
    {
        public int ID { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public int? FatherID { get; set; }

        [StringLength(255)]
        public string FatherName { get; set; }

        public int? OrderID { get; set; }

        [StringLength(255)]
        public string Des { get; set; }
    }
}
