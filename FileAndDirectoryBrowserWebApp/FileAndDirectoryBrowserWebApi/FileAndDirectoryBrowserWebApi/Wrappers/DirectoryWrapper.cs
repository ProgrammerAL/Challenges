using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileAndDirectoryBrowserWebApi.Wrappers
{
    public interface IDirectoryWrapper
    {
        bool DoesDirectoryExist(string path);
        ImmutableArray<string> LoadDirectoriesAtPath(string path);
        ImmutableArray<string> LoadFileNamesAtPath(string path);
    }

    public class DirectoryWrapper : IDirectoryWrapper
    {
        public bool DoesDirectoryExist(string path)
        {
            return Directory.Exists(path);
        }

        public ImmutableArray<string> LoadDirectoriesAtPath(string path)
        {
            var directoryInfo = new DirectoryInfo(path);
            var directoryNameInfos = directoryInfo.GetDirectories("*", SearchOption.TopDirectoryOnly);

            return directoryNameInfos.Select(x => x.Name).ToImmutableArray();
        }
        
        public ImmutableArray<string> LoadFileNamesAtPath(string path)
        {
            var directoryInfo = new DirectoryInfo(path);
            var filesNameInfos = directoryInfo.GetFiles("*", SearchOption.TopDirectoryOnly);

            return filesNameInfos.Select(x => x.Name).ToImmutableArray();
        }
    }
}
