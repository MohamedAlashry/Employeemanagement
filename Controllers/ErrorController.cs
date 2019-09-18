using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger _log;

        public ErrorController(ILogger<ErrorController> log)
        {
         _log = log;
        }
        [Route("/error/{statausCode}")]
        public IActionResult HandleHttpErrorStatusCode(int statusCode)
        {
            var HttpErrorFeatures = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                    _log.LogWarning(
                        $"404 error occured. Path = " + $"{HttpErrorFeatures.OriginalPath} and QueryString = " + $"{HttpErrorFeatures.OriginalQueryString}"
                        );
                    break;               
            }
         
            return View("CustomErrorPage", HttpErrorFeatures.OriginalPath);
        }

        [Route("/error")]
        public IActionResult ExceptionHandler()
        {
            var ExceptionFeatures = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _log.LogError($"The path {ExceptionFeatures.Path} " +  $"threw an exception {ExceptionFeatures.Error}");
            return View("ExceptionHandlerPage");
        }
    }
}