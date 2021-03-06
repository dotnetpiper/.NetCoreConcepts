﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace DotnetPiper.Filters
{
    public class CustomOneResourceFilter : IResourceFilter
    {
        private readonly ILogger _logger;

        public CustomOneResourceFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("CustomOneResourceFilter");
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            _logger.LogInformation("OnResourceExecuted");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            _logger.LogInformation("OnResourceExecuting");
            //context.Result = new ContentResult()
            //{
            //    Content = "Resource unavailable - pipeline has short circuited from ResourceFilter"
            //};
        }
    }
}
