using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace StaticFilesTut
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles(); // поддержка статических файлов

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello world");
            });
        }

        /// <summary>
        /// Простой пример использования статических файлов
        /// </summary>
        /// <param name="app"></param>
        public void SimpleStaticFilesExample(IApplicationBuilder app)
        {
            app.UseStaticFiles(); // поддержка статических файлов

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello world");
            });
        }
    }
}