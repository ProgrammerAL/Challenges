using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace FileAndDirectoryBrowserWebApi.Requests
{
    public class DirectoryInfoRequest
    {
        [FromQuery]
        [NotNull]
        [Required(AllowEmptyStrings = false)]
        public string? Path { get; set; }
    }
}
