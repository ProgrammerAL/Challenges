#pragma warning disable IDE0058 // Expression value is never used

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FileAndDirectoryBrowserWebApp.Options;
using FileAndDirectoryBrowserWebApp.ViewModels;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FileAndDirectoryBrowserWebApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddTransient<DirectoryViewModel>();

            builder.Services.AddSingleton<HostOptions>(services =>
            {
                var config = services.GetService<IConfiguration>()!;
                var configSection = config.GetSection(nameof(HostOptions));
                return new HostOptions(configSection);
            });

            await builder.Build().RunAsync();
        }
    }
}
#pragma warning restore IDE0058 // Expression value is never used
