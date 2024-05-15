using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json.Serialization;
using Univision.Fesp.API.Application.AutofacModules;
using Univision.Fesp.API.Application.Middlewares;
using Univision.Fesp.Beneficiario.API;
using System.Data;
using MultiTenancy.Business.Models.Singleton;
using MultiTenancy.Business.Interfaces;
using MultiTenancy.Business.Services;
using MultiTenancy.Data.Repositories;
using MultiTenancy.Data.Context;

namespace Univision.XUnitTest
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvcCore()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });

            services
                .AddHttpContextAccessor();

            services
                .AddCustomMVC(Configuration)
                .AddCustomDbContext(Configuration)
                .AddCustomOptions(Configuration)
                .AddCustomIntegrations(Configuration)
                .AddApiVersioning(o => o.ReportApiVersions = true);

            services
                .AddCustomVersionedApiExplorer(Configuration)
                .AddSwagger(Configuration)
                .AddSwaggerGen(c =>
                {
                    c.CustomSchemaIds((type) => type.FullName);
                });


            services
                .AddMemoryCache();

            services
                .ConfigureMediatR();

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.Providers.Add<Microsoft.AspNetCore.ResponseCompression.BrotliCompressionProvider>();
                options.EnableForHttps = true;
            });

            services.RegisterMultiTenancy(Configuration);
            services.RegisterServices();

            //configure autofac
            var container = new ContainerBuilder();
            container.Populate(services);

            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new ApplicationModule());
            container.RegisterModule(new Data.AutofacModules.ApplicationModule());

            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApiVersionDescriptionProvider provider, ILoggerProvider loggerProvider)
        {
            loggerFactory
               .AddProvider(loggerProvider);

            app.UseStaticFiles();
            app.UseRouting();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //   Use the HTTP Strict Transport Security Protocol (HSTS) Middleware.
                app.UseHsts();
            }

            // Use HTTPS Redirection Middleware to redirect HTTP requests to HTTPS.
            app.UseHttpsRedirection();

            // Use Cookie Policy Middleware to conform to EU General Data Protection Regulation (GDPR) regulations.
            app.UseCookiePolicy();

            app.UseResponseCompression();

            // Authenticate before the user accesses secure resources.
            // Setting to empty real (https://tools.ietf.org/html/rfc7235#section-4.1)            
            app.UseFactoryActivatedMiddleware();
            app.UseIdentifierHeaderMiddleware();

            app.UseAuthentication();

            app.UseCors("CorsPolicy");

            //app.UseMvcWithDefaultRoute();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app
                .UseSwagger()
                .UseSwaggerUI(c =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        c.SwaggerEndpoint(
                              $"/swagger/{description.GroupName}/swagger.json",
                              description.GroupName.ToUpperInvariant());
                    }

                    c.DocumentTitle = "Integração App";
                    c.RoutePrefix = string.Empty;
                });

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("HOME API");
            //});
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // Garante que as rotas dos controladores sejam usadas
            });
        }
    }
}
