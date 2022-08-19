using EFCoreTutorial.Common;
using EFCoreTutorial.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreTutorial.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentAddress> StudentAddresses { get; set; }

        /// <ÖZET>
        /// Konfigure edilirsen çağırılır
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Eğer  OnConfiguring 1 kere configure edilmişse true olarak işaretlenir.
            if (!optionsBuilder.IsConfigured)
            {
                // Dışardan setlenmemişse buraya düşer
                optionsBuilder.UseSqlServer(StringConstants.DbConnectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        /// <ÖZET>
        /// Mapleme işlemleri yapılır.
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            //modelBuilder.Entity<Student>().Property(x => x.Id)
            //    .HasColumnName("id")
            //    .HasColumnType("int");

            //modelBuilder.Entity<Student>().Property(x => x.FirstName)
            //    .HasColumnName("first_name")
            //    .HasColumnType("nvarchar")
            //    .HasMaxLength(100);

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("students");

                entity.Property(i => i.Id).HasColumnName("id").HasColumnType("int").UseIdentityColumn().IsRequired();
                entity.Property(i => i.FirstName).HasColumnName("first_name").HasColumnType("nvarchar").HasMaxLength(250);
                entity.Property(i => i.LastName).HasColumnName("last_name").HasColumnType("nvarchar").HasMaxLength(250);
                entity.Property(i => i.Number).HasColumnName("number");
                entity.Property(i => i.BirthDate).HasColumnName("birth_date");
                entity.Property(i => i.AddressId).HasColumnName("address_id");

                entity.HasMany(i => i.Books)
                    .WithOne(i => i.Student)
                    .HasForeignKey(i => i.StudentId)
                    .HasConstraintName("student_book_id_fk");


                modelBuilder.Entity<Teacher>(entity =>
                {
                    entity.ToTable("teachers");

                    entity.Property(i => i.Id).HasColumnName("id").UseIdentityColumn();
                    entity.Property(i => i.FirstName).HasColumnName("first_name").HasColumnType("nvarchar").HasMaxLength(100);
                    entity.Property(i => i.LastName).HasColumnName("last_name").HasColumnType("nvarchar").HasMaxLength(100);
                    entity.Property(i => i.BirthDate).HasColumnName("birth_date");
                });

                modelBuilder.Entity<Course>(entity =>
                {
                    entity.ToTable("courses");

                    entity.Property(i => i.Id).HasColumnName("id").UseIdentityColumn();
                    entity.Property(i => i.Name).HasColumnName("name").HasColumnType("nvarchar").HasMaxLength(100);
                    entity.Property(i => i.IsActive).HasColumnName("is_active");
                });

                modelBuilder.Entity<StudentAddress>(entity =>
                {
                    entity.ToTable("student_addresses");

                    entity.Property(i => i.Id).HasColumnName("id").UseIdentityColumn().ValueGeneratedOnAdd();
                    entity.Property(i => i.City).HasColumnName("city").HasMaxLength(50);
                    entity.Property(i => i.District).HasColumnName("district").HasMaxLength(100);
                    entity.Property(i => i.Country).HasColumnName("country").HasMaxLength(50);
                    entity.Property(i => i.FullAddress).HasColumnName("full_address").HasMaxLength(1000);

                    entity.HasOne(i => i.Student)
                        .WithOne(i => i.Address)
                        .HasForeignKey<Student>(i => i.AddressId)
                        .IsRequired(false)
                        .HasConstraintName("student_address_student_id_fk");
                });

            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
