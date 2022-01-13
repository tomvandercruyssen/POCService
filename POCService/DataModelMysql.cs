using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SharedLib.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;
namespace POCService
{
    public class EdgeDataContextMysql : DbContext
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
        //protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite(@"Data Source=C:\Users\tomcr\source\repos\POCService\POCService\edgedata.db; foreign keys=True;");
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=Azerty123;database=poc");
            }
        }

        public EdgeDataContextMysql() : base()
        {
           
        }
        public EdgeDataContextMysql(DbContextOptions options) : base(options)
        {

        }
    }
}
