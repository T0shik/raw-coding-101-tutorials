using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Middleware
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<HttpClientMiddleware>();

            services.AddHttpClient("name")
                .AddHttpMessageHandler<HttpClientMiddleware>()  // 1st
                .AddHttpMessageHandler<HttpClientMiddleware>(); // 2nd 
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseMiddleware<CustomMiddleware>();

            app.Use((request) =>
            {
                //do something before request
                //no access to httpcontext
                return request;
            });

            app.Use(async (httpContext, function) =>
            {
                // do something before 
                await function();
                // do something after
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }

    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // do before
            await _next(context);
            // do after
        }
    }
}
