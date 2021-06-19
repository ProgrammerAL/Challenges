using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FileAndDirectoryBrowserWebApi.Options;
using FileAndDirectoryBrowserWebApi.Responses;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Swashbuckle.AspNetCore.Annotations;

namespace FileAndDirectoryBrowserWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NavigateController : ControllerBase
    {
        private readonly ILogger<NavigateController> _logger;

        public NavigateController(
            ILogger<NavigateController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Finds the info for the root directory",
            OperationId = "NavigateController.GetRootDirectoryInfo")
        ]
        public async Task<ActionResult<RootDirectoryResponse>> GetRootDirectoryInfo()
        {
            //If path not found, throw exception
            //else return values
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Finds the info for a directory at a given path",
            OperationId = "NavigateController.GetDirectoryInfoAtPath")
        ]
        public async Task<ActionResult<DirectoryResponse>> GetDirectoryInfoAtPath([FromQuery] string path)
        {
            //If path not found, throw exception
            //else return values
        }
    }
}
