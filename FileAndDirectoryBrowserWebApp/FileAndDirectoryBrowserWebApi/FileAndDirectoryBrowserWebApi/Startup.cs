#pragma warning disable IDE0058 // Expression value is never used

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FileAndDirectoryBrowserWebApi.Middleware;
using FileAndDirectoryBrowserWebApi.Options;
using FileAndDirectoryBrowserWebApi.Services;
using FileAndDirectoryBrowserWebApi.Wrappers;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace FileAndDirectoryBrowserWebApi
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FileAndDirectoryBrowserWebApi", Version = "v1" });
            });

            services.AddOptions<NavigationOptions>()
                .Bind(Configuration.GetSection(nameof(NavigationOptions)))
                .ValidateDataAnnotations();

            services.AddSingleton<IDirectorySearchService, DirectorySearchService>();

            services.AddSingleton<IDirectoryWrapper, DirectoryWrapper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FileAndDirectoryBrowserWebApi v1"));

                //Hack: Has to be added twice in development for some reason
                app.UseMiddleware<HttpExceptionResponseMiddleware>();
            }

            app.UseMiddleware<HttpExceptionResponseMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            ForceLoadConfigOptions(app);
        }

        /// <summary>
        /// Loads all config options on app startup so validation exceptions happen now instead of mid-way through runtime
        /// </summary>
        private void ForceLoadConfigOptions(IApplicationBuilder app)
        {
            app.ApplicationServices.GetService<IOptions<NavigationOptions>>();
        }
    }
}
#pragma warning restore IDE0058 // Expression value is never used
