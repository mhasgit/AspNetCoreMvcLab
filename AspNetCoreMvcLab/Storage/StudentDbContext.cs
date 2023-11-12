using AspNetCoreMvcLab.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreMvcLab.Storage
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students => Set<Student>();

        public DbSet<Teacher> Teachers => Set<Teacher>();


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);

        //    optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=TestDb;Integrated Security=True;MultipleActiveResultSets=True");
        //}
    }
}
