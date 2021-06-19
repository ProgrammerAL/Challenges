using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace FileAndDirectoryBrowserWebApi.Responses
{
    public record DirectoryResponse(ImmutableArray<string> Directories, ImmutableArray<DirectoryResponse.FileSummaryResponse> Files)
    { 
        public record FileSummaryResponse(string FileName);
    }
}
