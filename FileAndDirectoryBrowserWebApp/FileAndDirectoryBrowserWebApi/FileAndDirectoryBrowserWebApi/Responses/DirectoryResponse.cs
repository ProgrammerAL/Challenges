using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace FileAndDirectoryBrowserWebApi.Responses
{
    public record DirectoryResponse(
        ImmutableArray<DirectoryResponse.DirectorySummaryResponse> Directories,
        ImmutableArray<DirectoryResponse.FileSummaryResponse> Files)
    {
        public record DirectorySummaryResponse(string Name);
        public record FileSummaryResponse(string Name);
    }
}
