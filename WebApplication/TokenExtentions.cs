using Microsoft.AspNetCore.Builder;

namespace WebApplication
{
    /// <summary>
    /// Класс расширения для добавления класса TokenMiddleware в приложения
    /// </summary>
    public static class TokenExtentions
    {
        public static IApplicationBuilder UseToken(this IApplicationBuilder app, string pattern)
        {
            return app.UseMiddleware<TokenMiddleware>(pattern);
        }
    }
}