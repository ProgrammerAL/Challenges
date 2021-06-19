using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using FileAndDirectoryBrowserWebApi.Exceptions.HttpExceptions;
using FileAndDirectoryBrowserWebApi.Options;
using FileAndDirectoryBrowserWebApi.Wrappers;

using Microsoft.Extensions.Options;

namespace FileAndDirectoryBrowserWebApi.Services
{
    public record DirectoryInfo(ImmutableArray<string> DirectoryNames, ImmutableArray<string> FileNames);

    public interface IDirectorySearchService
    {
        DirectoryInfo LoadRootDirectoryInfo();
        DirectoryInfo LoadDirectoryInfo(string path);
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

        public DirectoryInfo LoadRootDirectoryInfo()
        {
            var rootPath = _navigationConfig.StartDirectory;
            return LoadDirectoryInfoAtPath(rootPath);
        }

        public DirectoryInfo LoadDirectoryInfo(string path)
        {
            var rootPath = _navigationConfig.StartDirectory;
            var loadPath = Path.Combine(rootPath, path);
            return LoadDirectoryInfoAtPath(loadPath);
        }

        private DirectoryInfo LoadDirectoryInfoAtPath(string path)
        {
            EnsureDirectoryExists(path);

            var directoryNames = _directoryWrapper.LoadDirectoriesAtPath(path);
            var fileNames = _directoryWrapper.LoadFileNamesAtPath(path);

            return new DirectoryInfo(directoryNames, fileNames);
        }

        private void EnsureDirectoryExists(string path)
        {
            if (!_directoryWrapper.DoesDirectoryExist(path))
            {
                throw new NotFoundException($"Directory at path does not exist: {path}");
            }
        }
    }
}
