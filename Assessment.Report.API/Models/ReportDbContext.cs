using Microsoft.EntityFrameworkCore;

namespace Assessment.Report.API.Models
{
    public class ReportDbContext : DbContext
    {
        public const string DEFAULT_TABLE_NAME = "Assessment_Phonebook_Service";
        public ReportDbContext(DbContextOptions<ReportDbContext> options) : base(options)
        {

        }
        public DbSet<Report> Report { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReportDbContext).Assembly);
        }
    }
}
