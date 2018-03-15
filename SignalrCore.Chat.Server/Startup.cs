using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SignalrCore.Chat.Server.Hubs;

namespace SignalrCore.Chat.Server
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(corsPolicyBilder =>
            {
                corsPolicyBilder.AllowAnyHeader();
                corsPolicyBilder.AllowAnyMethod();
                corsPolicyBilder.AllowAnyOrigin();
            });

            app.UseSignalR(routeBuilder =>
            {
                routeBuilder.MapHub<ChatHub>("/chathub");
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Chat server running!");
            });
        }
    }
}