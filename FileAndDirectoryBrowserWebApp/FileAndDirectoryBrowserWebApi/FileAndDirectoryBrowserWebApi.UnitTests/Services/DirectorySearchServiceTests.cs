using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FileAndDirectoryBrowserWebApi.Exceptions.HttpExceptions;
using FileAndDirectoryBrowserWebApi.Options;
using FileAndDirectoryBrowserWebApi.Services;
using FileAndDirectoryBrowserWebApi.Wrappers;

using Microsoft.Extensions.Options;

using NSubstitute;

using Shouldly;

using Xunit;

namespace FileAndDirectoryBrowserWebApi.UnitTests.Services
{
    public class DirectorySearchServiceTests
    {
        private readonly IDirectoryWrapper _directoryWrapper;

        private readonly DirectorySearchService _service;

        public DirectorySearchServiceTests()
        {
            _directoryWrapper = Substitute.For<IDirectoryWrapper>();

            var navigationOptionsConfig = new NavigationOptions
            {
                StartDirectory = "c:/root"
            };

            var navigationOptions = Substitute.For<IOptions<NavigationOptions>>();
            _ = navigationOptions.Value.Returns(navigationOptionsConfig);

            _service = new DirectorySearchService(_directoryWrapper, navigationOptions);
        }

        [Fact]
        public void WhenRootDirectoryDoesNotExist_AssertException()
        {
            _ = _directoryWrapper.DoesDirectoryExist(default!).ReturnsForAnyArgs(false);

            _ = Should.Throw<NotFoundException>(() => _service.LoadRootDirectoryInfo());
        }

        [Fact]
        public void WhenRootDirectoryLoaded_AssertResult()
        {
            _ = _directoryWrapper.DoesDirectoryExist(default!).ReturnsForAnyArgs(true);

            _ = _directoryWrapper.LoadDirectoriesAtPath(default!)
                .ReturnsForAnyArgs(new[]
                {
                    "Dir1",
                    "Dir2",
                    "Dir3",
                }.ToImmutableArray());

            _ = _directoryWrapper.LoadFileNamesAtPath(default!)
                .ReturnsForAnyArgs(new[]
                {
                    "File1",
                    "File2",
                    "File3",
                }.ToImmutableArray());

            var result = _service.LoadRootDirectoryInfo();

            _ = result.ShouldNotBeNull();

            result.DirectoryNames.ShouldBeSubsetOf(new[] { "Dir1", "Dir2", "Dir3" });
            result.FileNames.ShouldBeSubsetOf(new[] { "File1", "File2", "File3" });
        }

        [Fact]
        public void WhenDirectoryDoesNotExist_AssertException()
        {
            _ = _directoryWrapper.DoesDirectoryExist(default!).ReturnsForAnyArgs(false);

            _ = Should.Throw<NotFoundException>(() => _service.LoadDirectoryInfo("/abc/mydir"));
        }

        [Fact]
        public void WhenDirectoryLoaded_AssertResult()
        {
            _ = _directoryWrapper.DoesDirectoryExist(default!).ReturnsForAnyArgs(true);

            _ = _directoryWrapper.LoadDirectoriesAtPath(default!)
                .ReturnsForAnyArgs(new[]
                {
                    "Dir1",
                    "Dir2",
                    "Dir3",
                }.ToImmutableArray());

            _ = _directoryWrapper.LoadFileNamesAtPath(default!)
                .ReturnsForAnyArgs(new[]
                {
                    "File1",
                    "File2",
                    "File3",
                }.ToImmutableArray());

            var result = _service.LoadDirectoryInfo("/abc/mydir");

            _ = result.ShouldNotBeNull();

            result.DirectoryNames.ShouldBeSubsetOf(new[] { "Dir1", "Dir2", "Dir3" });
            result.FileNames.ShouldBeSubsetOf(new[] { "File1", "File2", "File3" });
        }
    }
}
