using FluentValidation.AspNetCore;
using FruteiraApi.Core.Validations;
using FruteiraApi.Data.Contexts;
using FruteiraApi.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Reflection;

namespace FruteiraApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        #region Startup
        public Startup(IWebHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", optional: true,
                    reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }
        #endregion

        #region ConfigureServices
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<FrutaSelfValidation>());

            #region Contexts
            services.AddDbContext<FruteiraDbContext>(opt => opt.UseInMemoryDatabase("FruteiraProvider"));
            #endregion

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Api Fruteira e-Commerce",
                    Description = "Esta API tem como objetivo disponibilizar endpoints para manipulação de itens/produtos de uma fruteira.",
                    Version = "v1"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            ConfigureIoC(services);
        }
        #endregion

        #region Configure
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            ConfigureSwagger(app);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
        #endregion

        #region ConfigureIoC
        private void ConfigureIoC(IServiceCollection services)
        {
            services.ResolveDependencies();
        }
        #endregion

        #region ConfigureSwagger
        private void ConfigureSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "V1");
                c.DocumentTitle = "Api Fruteira e-Commerce";
                c.DocExpansion(DocExpansion.None);
            });
        }
        #endregion
    }
}
