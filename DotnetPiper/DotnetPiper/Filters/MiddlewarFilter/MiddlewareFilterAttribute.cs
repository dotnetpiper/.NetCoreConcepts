using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetPiper.Filters.MiddlewarFilter
{
    public class MiddlewareFilterAttribute : Attribute, IFilterFactory, IOrderedFilter  
    {
        public Type ConfigurationType { get; }
        public MiddlewareFilterAttribute(Type configurationType)
    {
        ConfigurationType = configurationType;
    }

   

        public bool IsReusable => throw new NotImplementedException();

        public int Order => throw new NotImplementedException();

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var middlewarePipelineService = serviceProvider.GetRequiredService<MiddlewareFilterBuilder>();
            var pipeline = middlewarePipelineService.GetPipeline(ConfigurationType);

            return new  MiddlewareFilter(pipeline);
        }
    }
}
