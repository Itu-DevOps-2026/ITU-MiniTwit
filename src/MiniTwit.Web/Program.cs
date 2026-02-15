using System.Security.Claims;
using MiniTwit.Core.Interfaces;
using MiniTwit.Infrastructure.Data;
using MiniTwit.Infrastructure.Entities;
using MiniTwit.Infrastructure.Repositories;
using MiniTwit.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication;
using MiniTwit.Web;
using MiniTwit.Web.Authentication;

// Load OAuth secrets
Env.Load();
ILogger<Program> logger = new LoggerFactory().CreateLogger<Program>();
var builder = WebApplication.CreateBuilder(args);

// Load database connection via configuration - from slides session 6
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MiniTwitDBContext>(options => options.UseSqlite(connectionString));

// Add EF Core Identity to the app
builder.Services.AddDefaultIdentity<Author>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = false; // disables special character requirement
        options.Password.RequiredLength = 6;
    }
).AddEntityFrameworkStores<MiniTwitDBContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<ICheepService, CheepService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddSingleton<LatestService>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<ICheepRepository, CheepRepository>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddAuthentication("Basic")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SimulatorOnly", policy =>
        policy.RequireClaim(ClaimTypes.Name, "simulator"));
});

bool gotClientId = Environment.GetEnvironmentVariable("AUTHENTICATION_GITHUB_CLIENTID") != null;
bool gotClientSecret = Environment.GetEnvironmentVariable("AUTHENTICATION_GITHUB_CLIENTSECRET") != null;

// Add OAuth to the App
if (gotClientId && gotClientSecret)
{
    builder.Services.AddAuthentication()
        .AddCookie()
        .AddGitHub(o =>
        {
            o.ClientId = Environment.GetEnvironmentVariable("AUTHENTICATION_GITHUB_CLIENTID")!;
            o.ClientSecret = Environment.GetEnvironmentVariable("AUTHENTICATION_GITHUB_CLIENTSECRET")!;
            o.CallbackPath = "/signin-github";
        });
}
else
{
    Console.WriteLine("Could not find Github Client ID and or Github Client Secret. OAuth with Github will not be available");
}

var app = builder.Build();

//Creates a scope to retrieve the database context, ensures the database exists, and seeds the database with example data
//Code from https://learn.microsoft.com/en-us/aspnet/core/data/ef-rp/intro?view=aspnetcore-8.0&tabs=visual-studio-code#seed-the-database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<MiniTwitDBContext>();
    context.Database.Migrate();
    
    var initializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    await initializer.SeedDatabase();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();


app.Run();