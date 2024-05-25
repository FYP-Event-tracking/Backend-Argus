using UserService_ArgusBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace UserService_ArgusBackend
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
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

    public class UserLoginDbContext : DbContext
    {
        public UserLoginDbContext(DbContextOptions<UserLoginDbContext> options) : base(options)
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
        public DbSet<UserLogin> UserLogin { get; set; }
    }
}