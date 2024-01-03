using Common.BaseCore.Models.Response;
using Common.BaseCore.Results.Concreate;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Common.BaseCore.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private ILogger<ExceptionHandlingMiddleware> _logger;
        private RequestDelegate _next;

        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        public ExceptionHandlingMiddleware(RequestDelegate next,ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                string errorType = exception.GetType().Name;
                string projectName=Assembly.GetEntryAssembly().GetName().Name;
                _logger.LogError(exception, exception.Message);
                httpContext.Response.ContentType = "application/json";
               // httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new BaseResponse(exception.Message, errorType, projectName), _jsonOptions));
            }
        }

    }
}
