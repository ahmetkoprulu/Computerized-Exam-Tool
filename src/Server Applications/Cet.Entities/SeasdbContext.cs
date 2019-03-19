using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Cet.Entities
{
    public partial class SeasdbContext : DbContext
    {
        public SeasdbContext()
        {
        }

        public SeasdbContext(DbContextOptions<SeasdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CourseOffering> CourseOfferings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<CourseOffering>(entity =>
            {
                entity.HasIndex(e => e.CourseId);

                entity.HasIndex(e => e.InstructorId);

                entity.Property(e => e.Semester)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Year)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}