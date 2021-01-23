using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebApplication.Services;

namespace WebApplication
{
    // Пример использования 
    // Если передать TimeService timeService в конструкторе и сохранить в переменную, то он будет создан 1 раз
    // и время не будет обновляться при обновлении страницы
    
    public class TimerMiddleware
    {
        // Вариант 1.
        // Без обновления времени
        
        // private readonly RequestDelegate _next;
        // private TimeService _timeService;
        //
        // public TimerMiddleware(RequestDelegate next, TimeService timeService)
        // {
        //     _next = next;
        //     _timeService = timeService;
        // }
        //
        // public async Task InvokeAsync(HttpContext context)
        // {
        //     var path = context.Request.Path.Value.ToLower();
        //     
        //     if (path == "/time")
        //     {
        //         context.Response.ContentType = "text/html; charset=utf-8";
        //         await context.Response.WriteAsync($"Текущее время: {_timeService?.Time}");
        //     }
        //     else
        //     {
        //         await _next.Invoke(context);
        //     }
        // }
        
        // Вариант 2.
        // С обновлением времени
        
        private readonly RequestDelegate _next;
        
        public TimerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, TimeService timeService)
        {
            var path = context.Request.Path.Value.ToLower();
            
            if (path == "/time")
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.WriteAsync($"Текущее время: {timeService?.Time}");
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}