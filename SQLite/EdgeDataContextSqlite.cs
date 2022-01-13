using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SharedLib.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace SQLite
{
    public class EdgeDataContextSqlite : DbContext
    {
        public DbSet<ServerCredentialsS> ServerCredentials { get; set; }
        public DbSet<AppUserS> AppUser { get; set; }
        public DbSet<TagS> Tag { get; set; }
        public DbSet<ServerS> Server { get; set; }
        public DbSet<ReadingS> Reading { get; set; }
        public DbSet<SupportedType> SupportedType { get; set; }
        public DbSet<Destination> Destination { get; set; }
        public DbSet<WebServiceGroup> WebServiceGroup { get; set; }
        public DbSet<WebService> WebService { get; set; }
        public DbSet<WebServiceElement> WebServiceElement { get; set; }
        public DbSet<WebServiceElementValue> WebServiceElementValue { get; set; }
        public DbSet<WebServiceCall> WebServiceCall { get; set; }
        public DbSet<GlobalConfig> GlobalConfig { get; set; }
        public DbSet<LogEntry> LogEntry { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite(@"Data Source=C:\Users\tomcr\source\repos\POCService\POCService\edgedata.db; foreign keys=True;");
        
        public EdgeDataContextSqlite() : base()
        {

        }
        public EdgeDataContextSqlite(DbContextOptions options) : base(options)
        {

        }
    }
}
