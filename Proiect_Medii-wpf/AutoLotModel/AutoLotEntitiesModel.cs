using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace AutoLotModel
{
    public partial class AutoLotEntitiesModel : DbContext
    {
        public AutoLotEntitiesModel()
            : base("name=AutoLotEntitiesModel")
        {
        }

        public virtual DbSet<Clocking> Clockings { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Timetracking> Timetrackings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .Property(e => e.Firstname)
                .IsFixedLength();

            modelBuilder.Entity<Employee>()
                .Property(e => e.Lastname)
                .IsFixedLength();

            modelBuilder.Entity<Employee>()
                .Property(e => e.Phone)
                .IsFixedLength();

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Clockings)
                .WithOptional(e => e.Employee)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Timetracking>()
                .Property(e => e.Month)
                .IsFixedLength();

            modelBuilder.Entity<Timetracking>()
                .Property(e => e.Hour)
                .IsFixedLength();

            modelBuilder.Entity<Timetracking>()
                .Property(e => e.Salary)
                .IsFixedLength();

            modelBuilder.Entity<Timetracking>()
                .HasMany(e => e.Clockings)
                .WithOptional(e => e.Timetracking)
                .WillCascadeOnDelete();
        }
    }
}
