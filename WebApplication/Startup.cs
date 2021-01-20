using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApplication
{
    public class Startup
    {
        private string name;

        public Startup()
        {
            name = "Паха";
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<TokenMiddleware>();
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello world!");
            });
        }

        /// <summary>
        /// Использование классов для обработки запроса
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        private void ClassMiddlewareExample(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<TokenMiddleware>();
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello world!");
            });
        }

        /// <summary>
        /// Использование Map для обработки запроса с определенным путем
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        private void MapExample(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Map("/index", (appBuilder) =>
            {
                appBuilder.Run(async (context) => { await context.Response.WriteAsync("<h2>Index page</h2>"); });
            });
            
            app.Map("/about", AboutHandler);

            app.Map("/home", (home) =>
            {
                home.Map("/index", (appBuilder) => // /home/index
                {
                    appBuilder.Run(async (context) => { await context.Response.WriteAsync("<h2>Home page</h2><h3>Index</h3>"); });
                });
            
                home.Map("/about", (appBuilder) => // home/about
                {
                    appBuilder.Run(async (context) => { await context.Response.WriteAsync("<h2>Home page</h2><h3>About</h3>"); });
                });
            });
            
            app.Run(async (context) =>
            {
                context.Response.ContentType = "text/html;charset=utf-8";
                
                await context.Response.WriteAsync("<h2>Not found</h2>");
            });
        }

        private void AboutHandler(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("<h2>About</h2>");
            });
        }

        /// <summary>
        /// Базовый пример hello world
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        private void HelloWorldExample(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // // Подключение middleware для работы со ститическими файлами
            // app.UseStaticFiles();

            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync($"Hello world!"); });
            });
        }

        /// <summary>
        /// Испозование Use и передача запроса дальше по конвееру обработки
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        private void UseExample(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var x = 2;
            var y = 3;
            var z = 0;
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, next) =>
            {
                z = x + y;
                await next();

                z += 100;
                await context.Response.WriteAsync($"z = {z}");
            });

            app.Run(async (context) =>
            {
                z += 10;
                await Task.FromResult(0);
            });
        }

        /// <summary>
        /// Использование метода для обработки запроса
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        private void HandlerMethodExample(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Можно расскомментить и запрос закончится в этом месте
            // app.Run(async (context) => { await context.Response.WriteAsync("hello"); });
            
            app.Run(Handler);
        }

        private async Task Handler(HttpContext context)
        {
            string host = context.Request.Host.Value;
            string path = context.Request.Path;
            string query = context.Request.QueryString.Value;

            context.Response.ContentType = "text/html;charset=utf-8";
                
            await context.Response.WriteAsync($"<h3>Хост: {host}</h3>" +
                                              $"<h3>Путь запроса: {path}</h3>" +
                                              $"<h3>Параметры строки запроса: {query}</h3>");
        }

        /// <summary>
        /// Получение свойств запроса
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        private void RequestFieldsExample(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Run(async (context) =>
            {
                string host = context.Request.Host.Value;
                string path = context.Request.Path;
                string query = context.Request.QueryString.Value;
            
                context.Response.ContentType = "text/html;charset=utf-8";
                
                await context.Response.WriteAsync($"<h3>Хост: {host}</h3>" +
                                                  $"<h3>Путь запроса: {path}</h3>" +
                                                  $"<h3>Параметры строки запроса: {query}</h3>");
            });
        }

        /// <summary>
        /// Простой запрос
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        private void SimpleResponseExample(IApplicationBuilder app, IWebHostEnvironment env)
        {
            int x = 2;

            app.Run(async (context) =>
            {
                x *= 2;
                await context.Response.WriteAsync($"x = {x}");
            });
        }
    }
}