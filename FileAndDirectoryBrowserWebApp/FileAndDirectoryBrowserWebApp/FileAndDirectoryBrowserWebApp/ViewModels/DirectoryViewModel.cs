using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using FileAndDirectoryBrowserWebApp.ApiResponses;
using FileAndDirectoryBrowserWebApp.Options;

namespace FileAndDirectoryBrowserWebApp.ViewModels
{
    public class DirectoryViewModel : ViewModelBase
    {
        private static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private readonly HostOptions _hostOptions;

        private bool _isErrored;
        private ImmutableArray<string> _directoryNames;
        private ImmutableArray<string> _fileNames;
        public DirectoryViewModel(HostOptions hostOptions)
        {
            _hostOptions = hostOptions;
            _isErrored = false;
            _directoryNames = ImmutableArray.Create<string>();
            _fileNames = ImmutableArray.Create<string>();
        }

        public bool IsErrored
        {
            get => _isErrored;
            private set => SetPropertyValue(ref _isErrored, value);
        }
        public ImmutableArray<string> DirectoryNames
        {
            get => _directoryNames;
            private set => SetPropertyValue(ref _directoryNames, value);
        }

        public ImmutableArray<string> FileNames
        {
            get => _fileNames;
            private set => SetPropertyValue(ref _fileNames, value);
        }

        public async Task LoadAsync(string? path)
        {
            using (var httpClient = new HttpClient())
            {
                var endpointUri = GenerateEndpointUrl(path);
                var result = await httpClient.GetAsync(endpointUri);
                await LoadFromResultAsync(result);
            }
        }

        private async Task LoadFromResultAsync(HttpResponseMessage result)
        {
            IsErrored = !result.IsSuccessStatusCode;

            if (result.IsSuccessStatusCode)
            {
                var resultJson = await result.Content.ReadAsStringAsync();
                var resultInfo = JsonSerializer.Deserialize<DirectoryInfoResponse>(resultJson, SerializerOptions);

                DirectoryNames = resultInfo?.Directories
                                    ?.Where(x => !string.IsNullOrWhiteSpace(x?.Name))
                                    .Select(x => x!.Name!)
                                    .ToImmutableArray()
                                    ?? ImmutableArray.Create<string>();

                FileNames = resultInfo?.Files
                                ?.Where(x => !string.IsNullOrWhiteSpace(x?.Name))
                                .Select(x => x!.Name!)
                                .ToImmutableArray()
                                ?? ImmutableArray.Create<string>();
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
