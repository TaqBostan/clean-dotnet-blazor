using Microsoft.AspNetCore.ResponseCompression;
using CleanDotnetBlazor.Application;
using CleanDotnetBlazor.Infrastructure;
using CleanDotnetBlazor.Server.Infrastructure;
using CleanDotnetBlazor.Infrastructure.Data;

namespace CleanDotnetBlazor.Server
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.AddApplicationServices();
            builder.AddInfrastructureServices();

            builder.Services.AddExceptionHandler<CustomExceptionHandler>();
            builder.Services.AddProblemDetails();
            builder.Services.AddRazorPages();

            var app = builder.Build();
            app.UseExceptionHandler("/Error");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
                await app.Services.InitialiseDatabaseAsync();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.MapRazorPages();
            app.MapEndpoints();
            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}