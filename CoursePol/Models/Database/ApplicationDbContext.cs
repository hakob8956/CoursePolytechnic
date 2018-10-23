using CoursePol.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
namespace CoursePol.Models
{

    public class ApplicationDbContext : IdentityDbContext<User>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseExercise> CourseExercises { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Folower> Folowers { get; set; }
        public DbSet<UserExercisesComplete> UserExercisesCompletes { get; set; }



    }
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

            var connectionString = configuration["ConnectionStrings:DefaultConnection"];

            builder.UseSqlServer(connectionString);

            return new ApplicationDbContext(builder.Options);
        }
    }
}




