using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiExplorerSettings(IgnoreApi =true)]
    [Route("errors/{statusCode}")]
    public class ErrorsController : ControllerBase
    {
        
        public IActionResult Errors(int statusCode)
        {
            return Ok(new ErrorResponse
            {
                IsError = true,
                UserMessage = "Requested resource not Found",
                StatusCode = statusCode
            });

        }
        
    }
}
