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

ILogger<Program> logger = new LoggerFactory().CreateLogger<Program>();
var builder = WebApplication.CreateBuilder(args);

// Load database connection via configuration - from slides session 6
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MiniTwitDBContext>(options => options.UseSqlite(connectionString));

// Add EF Core Identity to the app
builder.Services.AddDefaultIdentity<Author>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false; // disables special character requirement
        options.Password.RequiredLength = 1;
    }
).AddEntityFrameworkStores<MiniTwitDBContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddScoped<ICheepService, CheepService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddSingleton<LatestService>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<ICheepRepository, CheepRepository>();
builder.Services.AddAuthentication()
    .AddCookie().
    AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


 builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SimulatorOnly", policy =>
        policy.RequireClaim(ClaimTypes.Name, "simulator"));
});

var app = builder.Build();

//Creates a scope to retrieve the database context, ensures the database exists, and seeds the database with example data
//Code from https://learn.microsoft.com/en-us/aspnet/core/data/ef-rp/intro?view=aspnetcore-8.0&tabs=visual-studio-code#seed-the-database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<MiniTwitDBContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}   
else
{
    app.UseExceptionHandler("/Error");
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