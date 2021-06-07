namespace Smart.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public int ID { get; set; }

        [StringLength(255)]
        public string UserName { get; set; }

        [StringLength(255)]
        public string Password { get; set; }

        [StringLength(255)]
        public string TrueName { get; set; }

        public int? RoleID { get; set; }

        public int? categoryID { get; set; }

        [StringLength(255)]
        public string categoryName { get; set; }

        public int? Status { get; set; }

        [StringLength(255)]
        public string Des { get; set; }
    }
}
