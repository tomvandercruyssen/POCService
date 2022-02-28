using Microsoft.EntityFrameworkCore;
using SharedLib.Data;

namespace POCService.DataContexts.MySQL
{
    public class EdgeDataContext : DbContext
    {
        public DbSet<ServerCredentials> ServerCredentials { get; set; }
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<Server> Server { get; set; }
        public DbSet<Reading> Reading { get; set; }
        public DbSet<SupportedType> SupportedType { get; set; }
        public DbSet<Destination> Destination { get; set; }
        public DbSet<WebServiceGroup> WebServiceGroup { get; set; }
        public DbSet<WebService> WebService { get; set; }
        public DbSet<WebServiceElement> WebServiceElement { get; set; }
        public DbSet<WebServiceElementValue> WebServiceElementValue { get; set; }
        public DbSet<WebServiceCall> WebServiceCall { get; set; }
        public DbSet<GlobalConfig> GlobalConfig { get; set; }
        public DbSet<LogEntry> LogEntry { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=Azerty123;database=poc");                
            }
        }

        public EdgeDataContext() : base()
        {
           
        }
        public EdgeDataContext(DbContextOptions options) : base(options)
        {

        }
    }
}
