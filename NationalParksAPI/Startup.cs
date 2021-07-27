using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using NationalParksAPI.Data;
using NationalParksAPI.Repository;
using NationalParksAPI.Repository.IRepository;
using ParkyAPI.ParkyMapper;
using Swashbuckle.AspNetCore.SwaggerGen;
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
            services.AddApiVersioning(options => 
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });
            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("NationalParkRegistryV1", new OpenApiInfo 
            //    { 
            //        Title = "NationalParkRegistry", 
            //        Version = "v1" ,
            //        Description = "National Parks API",
            //        Contact = new OpenApiContact()
            //        {
            //            Name = "Faisal Alhazzani",
            //            Email = "F.Alhazzani@outlook.com",
            //            Url= new Uri("https://github.com/Faisal-Alhazzani")
            //        }
            //    });

                //c.SwaggerDoc("TrailsRegistryV1", new OpenApiInfo
                //{
                //    Title = "TrailsRegistry",
                //    Version = "v1",
                //    Description = "Trails API",
                //    Contact = new OpenApiContact()
                //    {
                //        Name = "Faisal Alhazzani",
                //        Email = "F.Alhazzani@outlook.com",
                //        Url = new Uri("https://github.com/Faisal-Alhazzani")
                //    }
                //});

            //    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, "NationalParksRegistry.xml");
            //    c.IncludeXmlComments(xmlCommentsFullPath);
            //});

            services.AddControllers();
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                foreach (var desc in provider.ApiVersionDescriptions) 
                {
                    c.SwaggerEndpoint($"swagger/{desc.GroupName}/swagger.json",
                    desc.GroupName.ToUpperInvariant());
                }
                c.RoutePrefix = "";
            }
            //app.UseSwaggerUI(c => {
            //    c.SwaggerEndpoint("/swagger/NationalParkRegistryV1/swagger.json", "National Park Regisrty v1");
            //    //c.SwaggerEndpoint("/swagger/TrailsRegistryV1/swagger.json", "Trails Regisrty v1");
            //    c.RoutePrefix = "";
            //}
                
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
