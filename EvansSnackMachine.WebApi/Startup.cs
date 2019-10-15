using EvansSnackMachine.Logic.Entities;
using EvansSnackMachine.Logic.Interfaces;
using EvansSnackMachine.Logic.ValueObjects;
using EvansSnackMachine.Persistence;
using EvansSnackMachine.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;

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

            //bson mappings
            BsonClassMap.RegisterClassMap<SnackMachine>(cm =>
            {
                cm.MapField(x => x.AmountInTransaction);
                cm.MapField(x => x.MoneyInside);
                cm.MapField(x => x.Slots);
            });

            BsonClassMap.RegisterClassMap<Money>(cm =>
            {
                cm.MapCreator(x => new Money(x.OneCentCount, x.TenCentCount, x.QuarterCount, x.OneDollarCount, x.FiveDollarCount, x.TwentyDollarCount));
                cm.MapProperty(x => x.OneCentCount);
                cm.MapProperty(x => x.TenCentCount);
                cm.MapProperty(x => x.QuarterCount);
                cm.MapProperty(x => x.OneDollarCount);
                cm.MapProperty(x => x.FiveDollarCount);
                cm.MapProperty(x => x.TwentyDollarCount);
                cm.MapProperty(x => x.Amount);
            });


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
