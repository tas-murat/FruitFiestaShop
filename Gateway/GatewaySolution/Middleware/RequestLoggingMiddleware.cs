using System.Text;

namespace GatewaySolution.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly  ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // Gelen isteği loglama işlemleri
            var request = context.Request;
            var requestBody = await ReadRequestBody(request);

            _logger.LogWarning($"Incoming Request: {request.Method} {request.Path} - Body: {requestBody}");
            // Sonraki middleware'e veya endpoint'e devam et
            await _next(context);
        }

        private async Task<string> ReadRequestBody(HttpRequest request)
        {
            // İstek body'sini oku ve geri döndür

            request.EnableBuffering();
            var body = string.Empty;

            using (var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
            {
                body = await reader.ReadToEndAsync();
                request.Body.Seek(0, SeekOrigin.Begin);
            }

            return body;
        }
    }

}
