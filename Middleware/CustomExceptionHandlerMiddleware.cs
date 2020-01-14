using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace alexander_neumann.api.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;

            // Handle known Exceptions
            //switch (exception)
            //{
            //    case ValidationException validationException:
            //        code = HttpStatusCode.BadRequest;
            //        result = JsonConvert.SerializeObject(validationException.Failures);
            //        break;
            //    case NotFoundException notFoundException:
            //        code = HttpStatusCode.NotFound;
            //        break;
            //    case UnauthorizedException unauthorizedException:
            //        code = HttpStatusCode.Unauthorized;
            //        break;
            //}

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (string.IsNullOrEmpty(result))
            {
                result = JsonConvert.SerializeObject(new { error = exception.Message });
            }

            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }

    public class XForwardedProtoHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public XForwardedProtoHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("X-Forwarded-Proto", out StringValues proto))
            {
                context.Request.Scheme = proto;
            }

            await _next(context);
        }
    }

    public static class XForwardedProtoHeaderMiddlewareExtensions
    {
        public static IApplicationBuilder UseXForwardedProtoHeaderMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<XForwardedProtoHeaderMiddleware>();
        }
    }
}
