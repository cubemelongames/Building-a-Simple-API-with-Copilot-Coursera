using Microsoft.Extensions.Primitives;

namespace UserManagementAPI.Middleware
{
    public class SimpleTokenAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<SimpleTokenAuthMiddleware> _logger;
        private readonly string _token;

        public SimpleTokenAuthMiddleware(RequestDelegate next, IConfiguration config, ILogger<SimpleTokenAuthMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            _token = config["ApiToken"] ?? "dev-token";
        }

        public async Task Invoke(HttpContext context)
        {
            // allow swagger + root without auth
            var path = context.Request.Path.Value?.ToLowerInvariant() ?? "";
            if (path.StartsWith("/swagger") || path == "/" || path.StartsWith("/favicon"))
            {
                await _next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue("Authorization", out StringValues auth) ||
                !auth.ToString().StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsJsonAsync(new { error = "Missing token." });
                return;
            }

            var provided = auth.ToString().Substring("Bearer ".Length).Trim();
            if (!string.Equals(provided, _token, StringComparison.Ordinal))
            {
                _logger.LogWarning("Invalid token");
                context.Response.StatusCode = 401;
                await context.Response.WriteAsJsonAsync(new { error = "Invalid token." });
                return;
            }

            await _next(context);
        }
    }
}