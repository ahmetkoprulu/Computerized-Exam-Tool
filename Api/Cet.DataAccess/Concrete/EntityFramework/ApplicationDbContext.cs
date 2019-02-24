using System;
using Cet.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Cet.DataAccess.Concrete.EntityFramework
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administrator> Administrators { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseOffering> CourseOfferings { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<ExamStatus> ExamStatuses { get; set; }
        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionType> QuestionTypes { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentCourseOffering> StudentCourseOfferings { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-CDI511I;Initial Catalog=SeasDb;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<Administrator>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Administrator)
                    .HasForeignKey<Administrator>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Administrators_Users");
            });

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.Property(e => e.Grade).HasMaxLength(10);

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(3000);

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Answers_Questions");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Answers_Students");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Courses_Departments");
            });

            modelBuilder.Entity<CourseOffering>(entity =>
            {
                entity.Property(e => e.Semester)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Year).HasColumnType("date");

                entity.Property(e => e.IsActive).HasMaxLength(50);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseOfferings)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseOfferings_Courses");

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.CourseOfferings)
                    .HasForeignKey(d => d.InstructorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseOfferings_Instructors");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Exam>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.CourseOffering)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.CourseOfferingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Exams_CourseOfferings");

                entity.HasOne(d => d.ExamStatus)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.ExamStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Exams_ExamStatuses");
            });

            modelBuilder.Entity<ExamStatus>(entity =>
            {
                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Instructor>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Instructors)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Instructors_Departments");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Instructor)
                    .HasForeignKey<Instructor>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Instructors_Users");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.Num)
                    .IsRequired();

                entity.Property(e => e.Extra)
                    .HasMaxLength(2000);

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.ExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Questions_Exams");

                entity.HasOne(d => d.QuestionType)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.QuestionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Questions_QuestionTypes");
            });

            modelBuilder.Entity<QuestionType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Students_Departments");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Student)
                    .HasForeignKey<Student>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Students_Users");
            });

            modelBuilder.Entity<StudentCourseOffering>(entity =>
            {
                entity.Property(e => e.RegistrationDate).HasColumnType("date");

                entity.HasOne(d => d.CourseOffering)
                    .WithMany(p => p.StudentCourseOfferings)
                    .HasForeignKey(d => d.CourseOfferingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentCourseOfferings_CourseOfferings");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentCourseOfferings)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentCourses_Students");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PasswordHash).IsRequired();

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}