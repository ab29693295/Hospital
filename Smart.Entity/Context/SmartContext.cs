namespace Smart.Entity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SmartContext : DbContext
    {
        public SmartContext()
            : base("name=SmartConnection")
        {
        }

        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Doctor> Doctor { get; set; }
        public virtual DbSet<OperationImage> OperationImage { get; set; }
        public virtual DbSet<OperationRecord> OperationRecord { get; set; }
        public virtual DbSet<OperationVideo> OperationVideo { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.FatherName)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.Des)
                .IsUnicode(false);

            modelBuilder.Entity<Doctor>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<Doctor>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<Doctor>()
                .Property(e => e.categoryName)
                .IsUnicode(false);

            modelBuilder.Entity<Doctor>()
                .Property(e => e.des)
                .IsUnicode(false);

            modelBuilder.Entity<OperationImage>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<OperationImage>()
                .Property(e => e.doctor)
                .IsUnicode(false);

            modelBuilder.Entity<OperationImage>()
                .Property(e => e.des)
                .IsUnicode(false);

            modelBuilder.Entity<OperationImage>()
                .Property(e => e.imagePath)
                .IsUnicode(false);

            modelBuilder.Entity<OperationRecord>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<OperationRecord>()
                .Property(e => e.categoryName)
                .IsUnicode(false);

            modelBuilder.Entity<OperationRecord>()
                .Property(e => e.hospNumber)
                .IsUnicode(false);

            modelBuilder.Entity<OperationRecord>()
                .Property(e => e.operativesite)
                .IsUnicode(false);

            modelBuilder.Entity<OperationRecord>()
                .Property(e => e.surgeon)
                .IsUnicode(false);

            modelBuilder.Entity<OperationRecord>()
                .Property(e => e.des)
                .IsUnicode(false);

            modelBuilder.Entity<OperationVideo>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<OperationVideo>()
                .Property(e => e.duration)
                .IsUnicode(false);

            modelBuilder.Entity<OperationVideo>()
                .Property(e => e.doctor)
                .IsUnicode(false);

            modelBuilder.Entity<OperationVideo>()
                .Property(e => e.des)
                .IsUnicode(false);

            modelBuilder.Entity<OperationVideo>()
                .Property(e => e.videoPath)
                .IsUnicode(false);

            modelBuilder.Entity<Patient>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<Patient>()
                .Property(e => e.hospNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Patient>()
                .Property(e => e.des)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.TrueName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.categoryName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Des)
                .IsUnicode(false);
        }
    }
}
