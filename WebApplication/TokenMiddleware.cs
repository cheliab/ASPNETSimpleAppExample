using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebApplication
{
    public class TokenMiddleware
    {
        private RequestDelegate _next;
        
        public TokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Query["token"];

            if (token != "123")
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Token is invalid");
            }
            else
                await _next(context);
        }
    }
}