namespace AutoLotModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Timetracking")]
    public partial class Timetracking
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Timetracking()
        {
            Clockings = new HashSet<Clocking>();
        }

        [Key]
        public int TimeId { get; set; }

        [StringLength(10)]
        public string Month { get; set; }

        [StringLength(10)]
        public string Hour { get; set; }

        [StringLength(10)]
        public string Salary { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Clocking> Clockings { get; set; }
    }
}
