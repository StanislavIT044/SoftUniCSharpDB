using System;

using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data
{
    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        {

        }

        public StudentSystemContext(DbContextOptions<StudentSystemContext> options)
            : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity
                    .HasKey(e => e.StudentId);

                entity
                    .Property(x => x.Name)
                    .IsRequired(true)
                    .HasMaxLength(100)
                    .IsUnicode(true);

                entity
                    .Property(x => x.PhoneNumber)
                    .IsRequired(false)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity
                    .Property(x => x.Birthday)
                    .IsRequired(false);
            });


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
