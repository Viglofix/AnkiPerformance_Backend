using BffService.Hubs;
using Duende.Bff.Yarp;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddBff().AddRemoteApis();
builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicies", policy =>
    {
        policy.
            WithOrigins("http://localhost:5173","http://localhost:5190")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
// Configure the authentication
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = "Cookies";
        options.DefaultChallengeScheme = "oidc";
        options.DefaultSignOutScheme = "oidc";
    })
    .AddCookie("Cookies",options=>
    {
        options.Cookie.Name = "BffCookie";
        options.Cookie.SameSite = SameSiteMode.Lax; // Adjust as needed
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.IsEssential = true;
    })
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = "http://localhost:5000"; // IdentityServer URL
        options.ClientId = "bff";
        options.ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";
        options.ResponseType = "code";

        options.GetClaimsFromUserInfoEndpoint = true;
        options.SaveTokens = true;
        options.MapInboundClaims = false;
        options.RequireHttpsMetadata = false;

        options.ReturnUrlParameter = "http://localhost:5155/signin-oidc";

        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.Scope.Add("AnkiCommunication");

        // Add this scope if you want to receive refresh tokens
        options.Scope.Add("offline_access");
        
    });

builder.Services.AddAuthorization();


var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("AllowAllPolicies");
app.UseAuthentication();

// adds antiforgery protection for local APIs
// it prevents CSRF attacks
app.UseBff();

// adds authorization for local and remote API endpoints
app.UseAuthorization();

app.MapGet("bff/log", async (IHttpClientFactory context) =>
{
    await context.CreateClient().PostAsync("http://localhost:5000//account/logout_post",null);
});
// login, logout, user, backchannel logout...
app.MapBffManagementEndpoints();
app.MapRemoteBffApiEndpoint("/ankiauthentication/login", "http://localhost:5190/AnkiAuthentication/login")
    .AllowAnonymous();

app.MapRemoteBffApiEndpoint("/ankiauthentication/getMessage", "http://localhost:5190/AnkiOperations/getMessage")
    .RequireAccessToken(Duende.Bff.TokenType.User);

app.MapRemoteBffApiEndpoint("/ankiauthentication/send_sentence", "http://localhost:5190/AnkiOperations/send_sentence")
    .RequireAccessToken(Duende.Bff.TokenType.User);

app.MapHub<AnkiHub>("/ankiHub");

app.Run();
