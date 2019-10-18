using ATM.Logic.Entities;
using ATM.Logic.Interfaces;
using ATM.Persistence;
using EvansSnackMachine.Logic.Entities;
using EvansSnackMachine.Logic.Interfaces;
using EvansSnackMachine.Logic.ValueObjects;
using EvansSnackMachine.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using Persistence.Shared;
using SharedKernel.ValueObjects;

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
            services.AddScoped<IATMRepository, ATMRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            this.RegisterMongoMappings();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void RegisterMongoMappings()
        {
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

            BsonClassMap.RegisterClassMap<Slot>(cm =>
            {         
                cm.MapCreator(x => new Slot(x.Position, x.SnackPile));
                cm.MapProperty(x => x.Position);
                cm.MapProperty(x => x.SnackPile);
            });

            BsonClassMap.RegisterClassMap<SnackPile>(cm =>
            {
                cm.MapCreator(x => new SnackPile(x.Snack, x.Quantity, x.Price));
                cm.MapProperty(x => x.Snack);
                cm.MapProperty(x => x.Quantity);
                cm.MapProperty(x => x.Price);
            });


            BsonClassMap.RegisterClassMap<Snack>(cm =>
            {
                cm.MapCreator(x => new Snack(x.Name));
                cm.MapProperty(x => x.Name);
            });

            BsonClassMap.RegisterClassMap<AutomatedTellerMachine>(cm =>
            {
                cm.MapCreator(x => new AutomatedTellerMachine(x.MoneyCharged, x.MoneyInside));
                cm.MapField(x => x.MoneyCharged);
                cm.MapField(x => x.MoneyInside);
            });
        }
    }
}
