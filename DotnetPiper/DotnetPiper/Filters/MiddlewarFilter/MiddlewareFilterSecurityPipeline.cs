﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace DotnetPiper.Filters.MiddlewarFilter
{
    public class MiddlewareFilterSecurityPipeline
    {
        public void Configure(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseSecurityMiddleware();
        }
    }
}
