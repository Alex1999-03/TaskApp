using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure
{
    public static class StartupSetup
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TaskAppContext>(options => options.UseSqlServer(connectionString, 
                x => x.MigrationsAssembly(typeof(TaskAppContext).Assembly.FullName)));
        }
    }
}
