using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();


var app = builder.Build();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
    name: "edit",
    pattern: "{controller=User}/{action=Edit}/{id?}");

//app.MapGet("/", () => "Hello World!");

app.Run();
