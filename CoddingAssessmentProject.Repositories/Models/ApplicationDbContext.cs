using Microsoft.EntityFrameworkCore;

namespace CoddingAssessmentProject.Repositories.Models
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                => optionsBuilder.UseNpgsql("Host=localhost;Database=CoddingAssessmentProject;Username=postgres; password=Tatva@123");
    }

}