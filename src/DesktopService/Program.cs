using DesktopService.Data;
using DesktopService.Services.contracts;
using DesktopService.Services.implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;
using OllamaSharp;
using Microsoft.AspNetCore.SignalR.Client;


DotNetEnv.Env.Load();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<DesktopServiceContext>(connection =>
{
    connection.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddChatClient(new OllamaApiClient(new Uri("http://localhost:11434/"), "deepseek-r1:8b"));
builder.Services.AddScoped<List<ChatMessage>>();
builder.Services.AddScoped<IDeepSeekTranslate, DeepSeekTranslate>();
builder.Services.AddScoped<IHubConnectionBuilder, HubConnectionBuilder>();
builder.Services.AddScoped<ISignalRConnection, SignalRConnection>();
builder.Services.AddScoped<IDesktopService, DesktopService.Services.implementations.DesktopService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
