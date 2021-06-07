namespace Smart.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OperationVideo")]
    public partial class OperationVideo
    {
        public int ID { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        [StringLength(255)]
        public string duration { get; set; }

        public DateTime? startTime { get; set; }

        public DateTime? endTime { get; set; }

        public int? operationID { get; set; }

        [StringLength(255)]
        public string doctor { get; set; }

        [StringLength(255)]
        public string des { get; set; }

        [StringLength(255)]
        public string videoPath { get; set; }
    }
}
