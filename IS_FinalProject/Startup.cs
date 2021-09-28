using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using TravelAgency.Data;
using TravelAgency.Models.Profiles;
using TravelAgency.Services.Abstraction;
using TravelAgency.Services.Services;
using Volo.Abp.Data;
using IS_FinalProject.Infrastructure;
using System.Reflection;
using System.IO;
using System;

namespace IS_FinalProject
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

            services.AddControllers();
            services.AddDbContext<TravelAgencyDbContext>((serviceProvider, options) =>
            {
                options.UseSqlServer(Configuration.GetSection("ConnectionStrings").Get<Infrastructure.ConnectionStrings>().DefaultConnection,
                    optionsBuilder =>
                    {
                        optionsBuilder.EnableRetryOnFailure();
                        optionsBuilder.CommandTimeout(60);
                        optionsBuilder.MigrationsAssembly("TravelAgency.Data");
                    });
                options
                    .UseInternalServiceProvider(serviceProvider)
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            }).AddEntityFrameworkSqlServer();

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(AgentProfile));
            }).CreateMapper();
            services.AddSingleton(mapper);

            services.AddTransient<IAgentService, AgentService>();
            services.AddTransient<IAirplaneService, AirplaneService>();
            services.AddTransient<IDestinationService, DestinationService>();
            services.AddTransient<IPassengerService, PassengerService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "IS_FinalProject",
                    Version = "v1",
                    Description="Travel Agency Project",
                    Contact =new OpenApiContact
                    {
                        Name="Maksim Miteski 4561",
                        Email="miteski10@gmail.com"
                    }
                });
                c.UseInlineDefinitionsForEnums();
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => { 

                c.SwaggerEndpoint("/swagger/v1/swagger.json", "IS_FinalProject v1");
                    c.DocumentTitle = "Internet Services Final Project API V1";
                    c.RoutePrefix = string.Empty;
            });
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
