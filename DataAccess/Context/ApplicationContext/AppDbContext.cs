using ApplicationCore.Entities.Concrete;
using DataAccess.SeedData.EntitySeedData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context.ApplicationContext
{
    public class AppDbContext : DbContext
    {
        static AppDbContext()
        {
            //Sizin yerel saatinizle uyumlu çalışarak, saat hatası vermeyi önler
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<CustomerManager> CustomerManagers { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CourseSeedData());
            modelBuilder.ApplyConfiguration(new CustomerManagerSeedData());
            modelBuilder.ApplyConfiguration(new TeacherSeedData());
            modelBuilder.ApplyConfiguration(new ClassroomSeedData());
            modelBuilder.ApplyConfiguration(new StudentSeedData());
        }
    }
}
