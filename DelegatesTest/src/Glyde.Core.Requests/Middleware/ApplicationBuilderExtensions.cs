﻿using Microsoft.AspNetCore.Builder;

namespace Glyde.Core.Requests.Middleware
{
    public static class ApplicationBuilderExtensions
    {
        
        public static IApplicationBuilder UseRequestContextGenerator(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequestContextGeneratorMiddleware>();
        }
    }
}