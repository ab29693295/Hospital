namespace Smart.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OperationImage")]
    public partial class OperationImage
    {
        public int ID { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        public int? operationID { get; set; }

        [StringLength(255)]
        public string doctor { get; set; }

        [StringLength(255)]
        public string des { get; set; }

        [StringLength(255)]
        public string imagePath { get; set; }
    }
}
