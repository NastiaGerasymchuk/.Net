using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace LabLastGer8
{
    public partial class LecturerContext : DbContext
    {
        //public LecturerContext()
        //{
        //}

        public LecturerContext(DbContextOptions<LecturerContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Lecturer> Lecturers { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<PlacesWork> PlacesWorks { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Stavka> Stavkas { get; set; }
        public virtual DbSet<SubLecturer> SubLecturers { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {

        //        optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Lecturer;Trusted_Connection=True;");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.HouseNumber).HasMaxLength(50);

                entity.Property(e => e.Street).HasMaxLength(50);
            });

            modelBuilder.Entity<Lecturer>(entity =>
            {
                entity.Property(e => e.Characteristic).HasMaxLength(50);

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Lecturers)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Lecturers_Persons");

                entity.HasOne(d => d.PlaceWork)
                    .WithMany(p => p.Lecturers)
                    .HasForeignKey(d => d.PlaceWorkId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Lecturers_PlacesWork");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Lecturers)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Lecturers_Posts");

                entity.HasOne(d => d.Stavka)
                    .WithMany(p => p.Lecturers)
                    .HasForeignKey(d => d.StavkaId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Lecturers_Stavka");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Persons_Addresses");
            });

            modelBuilder.Entity<PlacesWork>(entity =>
            {
                entity.ToTable("PlacesWork");

                entity.Property(e => e.PlaceName).HasMaxLength(50);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.PostName).HasMaxLength(50);
            });

            modelBuilder.Entity<Stavka>(entity =>
            {
                entity.ToTable("Stavka");
            });

            modelBuilder.Entity<SubLecturer>(entity =>
            {
                entity.HasOne(d => d.Lecturer)
                    .WithMany(p => p.SubLecturers)
                    .HasForeignKey(d => d.LecturerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_SubLecturers_Lecturers");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.SubLecturers)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_SubLecturers_Subjects");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.Property(e => e.SubjName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
