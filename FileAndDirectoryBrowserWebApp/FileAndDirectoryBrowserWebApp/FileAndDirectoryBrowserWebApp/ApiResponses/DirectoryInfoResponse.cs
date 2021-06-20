using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace FileAndDirectoryBrowserWebApp.ApiResponses
{
    public class DirectoryInfoResponse
    {
        public DirectorySummaryResponse?[]? Directories { get; set; }
        public FileSummaryResponse?[]? Files { get; set; }

        public class DirectorySummaryResponse
        {
            public string? Name { get; set; }
        }

        public class FileSummaryResponse
        {
            public string? Name { get; set; }
        }
    }
}
