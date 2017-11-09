using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DotnetPiper.Filters;
using DotnetPiper.Filters.ActionFilters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DotnetPiper.Controllers
{
    //[Route("api/[controller]")]
    public class TestController : Controller
    {
        private readonly ILogger _logger;
        public TestController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("Filters Execution from Controller");
        }
        // GET: api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        [HttpGet]
        [ServiceFilter(typeof(CustomLogActionOneFilter))]
        [ServiceFilter(typeof(CustomOneLoggingExceptionFilter))]
        [ServiceFilter(typeof(CustomOneResourceFilter))]
        public ActionResult GetDetail()
        {
            _logger.LogInformation("Executing Http GetDetail action...");
            return Content("Data sent from GetDetail Action of Test Controller during usage of ResourceFilter...");
        }

    }
}
