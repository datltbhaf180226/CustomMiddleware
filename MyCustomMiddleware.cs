using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace C_fundamental.netcore
{
    public class MyCustomMiddleware
    {
        private readonly RequestDelegate next;
        public MyCustomMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var time = new Stopwatch();
            time.Start();
            httpContext.Response.OnStarting(() =>
            {
                httpContext.Response.OnStarting(() =>
                  {
                      httpContext.Response.Headers.Add("Dat", "hello");
                      return Task.FromResult(0);
                  });
                time.Stop();
                var responseTimeForCompleteRequest = time.ElapsedMilliseconds;
                httpContext.Response.WriteAsync($"The processing take {responseTimeForCompleteRequest.ToString()} millisecond.");
                return Task.CompletedTask;
            });
            await next.Invoke(httpContext);
        }
    }
}