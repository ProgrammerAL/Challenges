using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace FileAndDirectoryBrowserWebApi.Options
{
    public class NavigationOptions
    {
        [NotNull]
        [Required(AllowEmptyStrings = false)]
        public string? StartDirectory { get; set; }
    }
}
