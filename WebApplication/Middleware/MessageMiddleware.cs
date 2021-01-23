using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebApplication.Services;

namespace WebApplication
{
    public class MessageMiddleware
    {
        private readonly RequestDelegate _next;
        private MessageService _messageService;

        public MessageMiddleware(RequestDelegate next, MessageService messageService)
        {
            _next = next;
            _messageService = messageService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.ContentType = "text/html; charset=utf-8";
            await context.Response.WriteAsync($"Сообщение: {_messageService.GetMessage()}");
        }

    }
}