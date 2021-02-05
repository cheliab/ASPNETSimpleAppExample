using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EnvironmentVarTut
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Run(async (context) =>
            {
                context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";

                if (env.IsEnvironment("Test")) // Если проект в состоянии "Test"
                {
                    await context.Response.WriteAsync("В состоянии тестирования");
                }
                else if(env.IsDevelopment())
                {
                    await context.Response.WriteAsync("В процессе разработки");
                }
                else
                {
                    await context.Response.WriteAsync("В продакшене");
                }
            });
        }
    }
}