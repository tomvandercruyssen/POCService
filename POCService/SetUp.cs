using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace POCService
{
    public class SetUp
    {
        public SetUp()
        {
            ConfigureServices();
        }

        public void ConfigureServices()
        {
            //services.AddDbContext<EdgeDataContext>(options => options.UseSqlite("Data Source=edgedata.db; foreign keys=True;"), ServiceLifetime.Transient, ServiceLifetime.Singleton);
           
        }

        public void Configure(EdgeDataContext context)
        {
            context.Database.Migrate();
        }

    }
}
