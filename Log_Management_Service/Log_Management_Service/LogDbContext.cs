using LogService_ArgusBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace LogService_ArgusBackend
{
    public class LogDbContext : DbContext
    {
        public LogDbContext(DbContextOptions<LogDbContext> options) : base(options)
        {
            try
                {
                    var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                    if (databaseCreator != null)
                    {
                        if (!databaseCreator.CanConnect())
                        {
                            databaseCreator.Create();
                        }
                        if (!databaseCreator.HasTables())
                        {
                            databaseCreator.CreateTables();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
        }
        public DbSet<Log> Logs { get; set; }
    }
}