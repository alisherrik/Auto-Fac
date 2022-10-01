using Auto_Fac.Models;
using Auto_Fac.Models.Faculty;
using Auto_Fac.Models.Faculty.Professions;
using Microsoft.EntityFrameworkCore;

namespace Auto_Fac
{
    public class AppDbContext :DbContext
    {
        
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<admin> Admins { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Departament> Departaments { get; set; }
        public DbSet<LessonSchedule> LessonSchedule { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<Simesters> Simesters { get; set; }
        public DbSet<WeekDays> WeekDays { get; set; }
        public  DbSet<day> days { get; set; }
    }
}