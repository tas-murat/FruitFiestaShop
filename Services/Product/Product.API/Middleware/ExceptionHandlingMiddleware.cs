using Product.Application.Response;
using System.Reflection;
using System.Text.Json;

namespace Product.API.Middleware
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
                string message= exception.Message;
                string projectName=Assembly.GetEntryAssembly().GetName().Name;
                _logger.LogError(exception, message);
                httpContext.Response.ContentType = "application/json";
                // httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new BaseResponse(exception.Message, errorType, projectName), _jsonOptions));
            }
        }

    }
}
