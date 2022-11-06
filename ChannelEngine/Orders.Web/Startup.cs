using ChanelEngine.Service;
using ChanelEngine.Service.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orders.Business.Interfaces;
using Orders.Business.RequestHandlers;
using Orders.ChanelEngine.Service;
using Orders.ChanelEngine.Service.Interfaces;
using Orders.Common;
using Orders.Common.Hosting.Extensions;
using Orders.Common.Interfaces;

namespace Orders.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddScoped<IApplicationConfiguration, WebAppConfiguration>()
                    .AddScoped<IRequestHandlerFactory, RequestHandlerFactory>()
                    .AddScoped<IChannelEngineService, ChannelEngineService>()
                    .AddScoped<IChannelEngineWebClient, ChannelEngineWebClient>()
                    .RegisterRequestHandler();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
