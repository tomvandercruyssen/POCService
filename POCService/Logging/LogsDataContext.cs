using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace POCService.Logging
{
    public class LogsDataContext : DbContext
    {
        public DbSet<Log> Logs { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite(@"Data Source=D:/Csharp/POCServiceTest/LogPOCdata.db;");

        public LogsDataContext() : base()
        {
        }
        public LogsDataContext(DbContextOptions options) : base(options)
        {

        }
    }
}
