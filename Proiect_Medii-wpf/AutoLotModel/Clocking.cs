namespace AutoLotModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Clocking")]
    public partial class Clocking
    {
        [Key]
        public int ClockId { get; set; }

        public int? EmpId { get; set; }

        public int? TimeId { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Timetracking Timetracking { get; set; }
    }
}
