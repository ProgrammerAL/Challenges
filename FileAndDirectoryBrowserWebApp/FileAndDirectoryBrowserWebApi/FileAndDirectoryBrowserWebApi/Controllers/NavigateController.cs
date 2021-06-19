using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using FileAndDirectoryBrowserWebApi.Exceptions.HttpExceptions;
using FileAndDirectoryBrowserWebApi.Requests;
using FileAndDirectoryBrowserWebApi.Responses;
using FileAndDirectoryBrowserWebApi.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Swashbuckle.AspNetCore.Annotations;

namespace FileAndDirectoryBrowserWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NavigateController : ControllerBase
    {
        private readonly IDirectorySearchService _directorySearchService;
        private readonly ILogger<NavigateController> _logger;

        public NavigateController(
            IDirectorySearchService directorySearchService,
            ILogger<NavigateController> logger)
        {
            _directorySearchService = directorySearchService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RootDirectoryResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Finds the info for the root directory",
            OperationId = "NavigateController.GetRootDirectoryInfo")
        ]
        public ActionResult<RootDirectoryResponse> GetRootDirectoryInfo()
        {
            var info = _directorySearchService.LoadRootDirectoryInfo();
            var response = new RootDirectoryResponse(
                info.DirectoryNames.Select(x => new RootDirectoryResponse.DirectorySummaryResponse(Name: x)).ToImmutableArray(),
                info.FileNames.Select(x => new RootDirectoryResponse.FileSummaryResponse(Name: x)).ToImmutableArray());

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DirectoryResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Finds the info for a directory at a given path",
            OperationId = "NavigateController.GetDirectoryInfoAtPath")
        ]
        public ActionResult<DirectoryResponse> GetDirectoryInfoAtPath(DirectoryInfoRequest infoRequest)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelValidationException("Model Invalid");
            }

            var info = _directorySearchService.LoadDirectoryInfo(infoRequest.Path);
            var response = new DirectoryResponse(
                info.DirectoryNames.Select(x => new DirectoryResponse.DirectorySummaryResponse(Name: x)).ToImmutableArray(),
                info.FileNames.Select(x => new DirectoryResponse.FileSummaryResponse(Name: x)).ToImmutableArray());

            return Ok(response);
        }
    }
}
