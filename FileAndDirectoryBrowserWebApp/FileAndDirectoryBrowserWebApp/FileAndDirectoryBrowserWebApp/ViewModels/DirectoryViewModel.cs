using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using FileAndDirectoryBrowserWebApp.Options;

using Microsoft.Extensions.Configuration;

namespace FileAndDirectoryBrowserWebApp.ViewModels
{
    public class DirectoryViewModel : ViewModelBase
    {
        private readonly HostOptions _hostOptions;
        public DirectoryViewModel(HostOptions hostOptions)
        {
            _hostOptions = hostOptions;
        }

        public bool IsErrored { get; private set; }

        public async Task LoadAsync(string? path)
        {
            using (var httpClient = new HttpClient())
            {
                var endpointUri = GenerateEndpointUrl(path);
                var result = await httpClient.GetAsync(endpointUri);
                LoadFromResult(result);
            }
        }

        private void LoadFromResult(HttpResponseMessage result)
        {
            IsErrored = !result.IsSuccessStatusCode;

            if (result.IsSuccessStatusCode)
            { 
            }
        }

        private string GenerateEndpointUrl(string? path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return GenerateRootDirectoryUrl();
            }

            return GenerateUrlAtPath(path);
        }

        private string GenerateRootDirectoryUrl()
            => _hostOptions.NavigationRootEndpoint;

        private string GenerateUrlAtPath(string path)
            => $"{_hostOptions.NavigationAtPathEndpoint}?path={path}";
    }
}
