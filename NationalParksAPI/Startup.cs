using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NationalParksAPI.Data;
using NationalParksAPI.Repository;
using NationalParksAPI.Repository.IRepository;
using ParkyAPI.ParkyMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NationalParksAPI
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
            services.AddDbContextPool<NationalParksDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ParksDBConnection")));
            services.AddScoped<INationalParkRepository, NationalParkRepository>();
            services.AddScoped<ITrailRepository, TrailRepository>();
            services.AddAutoMapper(typeof(ParkyMappings));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("NationalParkRegistryV1", new OpenApiInfo { 
                    Title = "NationalParkRegistry", 
                    Version = "v1" ,
                    Description = "National Parks API",
                    Contact = new OpenApiContact()
                    {
                        Name = "Faisal Alhazzani",
                        Email = "F.Alhazzani@outlook.com",
                        Url= new Uri("https://github.com/Faisal-Alhazzani")
                    }
                });
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, "NationalParksRegistry.xml");
                c.IncludeXmlComments(xmlCommentsFullPath);
            });
            services.AddControllers();
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/NationalParkRegistryV1/swagger.json", "National Park Regisrty v1");
                c.RoutePrefix = "";
            }
                
            );
             
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
