using ArgusBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace ArgusBackend
{
    public class ArgusDbContext : DbContext
    {
        public ArgusDbContext(DbContextOptions<ArgusDbContext> options) : base(options)
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

        public DbSet<User> Users { get; set; }
    }
}