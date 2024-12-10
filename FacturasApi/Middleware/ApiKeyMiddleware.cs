namespace FacturasApi.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string ApiKeyHeaderName = "X-API-KEY";
        private const string ApiKey = "claveSuperSecreta";

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey) ||
                extractedApiKey != ApiKey)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Problemas Internos");
                return;
            }

            await _next(context);
        }
    }
}
