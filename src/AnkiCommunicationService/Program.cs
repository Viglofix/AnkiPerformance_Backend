using AnkiCommunicationService.Drivers;
using AnkiCommunicationService.POM;
using AnkiCommunicationService.Services.Contracts;
using AnkiCommunicationService.Services.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Playwright;

DotNetEnv.Env.Load();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Playwright services
builder.Services.AddSingleton<IPage>(provider => AnkiDriver.CreateAnkiDriver().Page);

builder.Services.AddTransient<IAnkiPageAuthentication, AnkiPageAuthentication>();
builder.Services.AddTransient<IAnkiPageOperations, AnkiPageOperations>();
builder.Services.AddScoped<AnkiPageAuthenticationPom>();
builder.Services.AddScoped<AnkiPageOperationsPom>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicies", policy =>
    {
        policy.
            WithOrigins("http://localhost:5173","http://localhost:5155")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Bearer",options =>
    {
        options.Authority = builder.Configuration["IdentityServiceUrl"];
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = false // Validate
        };
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseCors("AllowAllPolicies");
app.UseAuthorization();

app.MapControllers();

app.Run();

