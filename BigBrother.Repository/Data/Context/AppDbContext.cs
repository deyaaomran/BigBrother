using BigBrother.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.Repository.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourses>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId }); // Composite Key

            modelBuilder.Entity<Attendance>()
            .HasOne(a => a.studentCourses)           
            .WithMany(s => s.attendances)
            .HasForeignKey(sc => new { sc.StudentId, sc.CourseId });


        }

        public DbSet<Student> students { get; set; }
        public DbSet<Attendance> attendances { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<StudentCourses> studentCourses { get; set; }
        public DbSet<Instructor> instructors { get; set; }
        public DbSet<Asisstant> asisstants { get; set; }

    }
}
