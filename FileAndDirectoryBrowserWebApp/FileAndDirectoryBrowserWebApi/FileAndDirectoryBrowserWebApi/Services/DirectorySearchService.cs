using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileAndDirectoryBrowserWebApi.Options;
using FileAndDirectoryBrowserWebApi.Responses;
using FileAndDirectoryBrowserWebApi.Wrappers;

using Microsoft.Extensions.Options;

namespace FileAndDirectoryBrowserWebApi.Services
{
    public interface IDirectorySearchService
    {
        Task<RootDirectoryResponse> LoadRootDirectoryResponseAsync();
    }

    public class DirectorySearchService : IDirectorySearchService
    {
        private readonly IDirectoryWrapper _directoryWrapper;
        private readonly NavigationOptions _navigationConfig;

        public DirectorySearchService(
            IDirectoryWrapper directoryWrapper,
            IOptions<NavigationOptions> navigationOptions)
        {
            _directoryWrapper = directoryWrapper;
            _navigationConfig = navigationOptions.Value;
        }

        public async Task<RootDirectoryResponse> LoadRootDirectoryResponseAsync()
        { 
        }
    }
}
