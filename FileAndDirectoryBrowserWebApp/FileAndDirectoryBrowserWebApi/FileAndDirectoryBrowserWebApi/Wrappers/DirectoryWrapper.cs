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
            return Directory.GetDirectories(path).ToImmutableArray();
        }
        
        public ImmutableArray<string> LoadFileNamesAtPath(string path)
        {
            return Directory.GetFiles(path).ToImmutableArray();
        }
    }
}
