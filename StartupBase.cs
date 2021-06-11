using Microsoft.Extensions.DependencyInjection;
using System;

namespace ProjectThijsChris
{
    public class StartupBase
    {

        // This method gets called by the runtime. Use this method to add services to the container.
    

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.Cookie.Name = "MyCookie";
                options.IdleTimeout = NewMethod();
                options.Cookie.IsEssential = true;
            });

            services.AddControllersWithViews();
        }

        private static TimeSpan NewMethod()
        {
            return TimeSpan.FromSeconds(60);
        }
    }
}