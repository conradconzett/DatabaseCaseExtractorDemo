using Microsoft.EntityFrameworkCore;

namespace ExportHandler
{
    public class ExportDatabaseContext : DbContext
    {
        public ExportDatabaseContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<ExportModel> Exports { get; set; }

    }
}
