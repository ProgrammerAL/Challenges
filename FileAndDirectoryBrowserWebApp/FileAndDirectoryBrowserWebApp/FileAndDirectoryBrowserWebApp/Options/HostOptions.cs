using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

namespace FileAndDirectoryBrowserWebApp.Options
{
    public class HostOptions
    {
        public HostOptions(IConfiguration config)
        {
            NavigationRootEndpoint = config[nameof(NavigationRootEndpoint)] ?? throw new ArgumentNullException(nameof(NavigationRootEndpoint));
            NavigationAtPathEndpoint = config[nameof(NavigationAtPathEndpoint)] ?? throw new ArgumentNullException(nameof(NavigationAtPathEndpoint));
        }

        public string NavigationRootEndpoint { get; }

        public string NavigationAtPathEndpoint { get; }
    }
}
