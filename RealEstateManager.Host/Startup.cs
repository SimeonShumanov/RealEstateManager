using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealEstateManager.BL.Interfaces;
using RealEstateManager.BL.Services;
using RealEstateManager.DL.Interfaces;
using RealEstateManager.DL.Repositories.InMemoryRepos;
using RealEstateManager.DL.Repositories.MongoRepos;
using RealEstateManager.Host.Extensions;
using RealEstateManager.Models.Configuration;
using FluentValidation.AspNetCore;
using Serilog;
using ILogger = Serilog.ILogger;

namespace RealEstateManager
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
            services.AddSingleton(Log.Logger);

            services.AddSingleton<IHouseRepository, HouseMongoRepository>();
            services.AddSingleton<IApartmentRepository, ApartmentMongoRepository>();
            services.AddSingleton<IwarehouseRepository, warehouseMongoRepository>();



            services.AddSingleton<IHouseService, HouseService>();
            services.AddSingleton<IApartmentService, ApartmentService>();
            services.AddSingleton<IwarehouseService, warehouseService>();


            services.AddAutoMapper(typeof(Startup));

            services.Configure<MongoDbConfig>(Configuration.GetSection(nameof(MongoDbConfig)));

            services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RealEstateManager", Version = "v1" });
            });

            services.AddHealthChecks();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RealEstateManager v1"));
            }

            app.ConfigureExceptionHandler(logger);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
