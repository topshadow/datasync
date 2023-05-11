using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using RongChengApp.Data;
using System;
using RongChengApp.Services;
using System.Net;
var builder = WebApplication.CreateBuilder(args);
// var ip = IPAddress.Parse("127.0.0.1");
// var hostname = Dns.GetHostName();
// Console.WriteLine(hostname);
// Add services to the container.
builder.Services.AddHttpClient();

builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<ConfigService>();
builder.Services.AddSingleton<AutoLoginService>();
builder.Services.AddCors(opt => opt.AddPolicy("all", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<UtilService>();
builder.Services.AddMasaBlazor(options =>
{
    options.ConfigureTheme(theme =>
 {
     theme.Dark = false;
 });
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "医疗第三方接口对接",
        Description = "An ASP.NET Core Web API for managing ToDo items",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new Microsoft.OpenApi.Models.OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });

    // using System.Reflection;
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// app.UseHttpsRedirection();


app.UseStaticFiles();
// app.UseCors("all");

app.UseRouting();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// app.MapControllers();
app.MapBlazorHub();
// app.MapControllers();

app.MapFallbackToPage("/_Host");


app.Run();
