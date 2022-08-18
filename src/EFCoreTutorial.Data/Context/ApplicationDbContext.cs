using EFCoreTutorial.Data.Model;
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
                optionsBuilder.UseSqlServer("Data Source=BIO-DEV-09\\SQLEXPRESS;Initial Catalog=EFCoreTutorial;Persist Security Info=True;User ID=sa;Password=Srv12345");
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
                //entity.Property(i => i.BirthDate).HasColumnName("birth_date");
                entity.Property(i => i.AddressId).HasColumnName("address_id");

                //entity.HasMany(i => i.Books)
                //    .WithOne(i => i.Student)
                //    .HasForeignKey(i => i.StudentId)
                //    .HasConstraintName("student_book_id_fk");


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

            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
