using EvansSnackMachine.Logic.Interfaces;
using EvansSnackMachine.Persistence;
using EvansSnackMachine.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace EvansSnackMachine.WebApi
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
            // requires using Microsoft.Extensions.Options
            services.Configure<SnackMachineDatabaseSettings>(
                Configuration.GetSection(nameof(SnackMachineDatabaseSettings)));

            services.AddSingleton<ISnackMachineDatabaseSettings>(sp =>
           sp.GetRequiredService<IOptions<SnackMachineDatabaseSettings>>().Value);

            services.AddControllers();

            services.AddScoped<ISnackMachineRepository, SnackMachineRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
