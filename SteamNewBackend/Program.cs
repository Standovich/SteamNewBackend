using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SteamNewBackend.Database;
using SteamNewBackend.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MariaDbConnectionString");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MariaDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
builder.Services.AddScoped<IDeveloperService, DeveloperService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IUserGameService, UserGameService>();

builder.Services.AddAuthentication(a =>
{
    a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("jkosv.dklsvdv@dauls")),
        ValidateAudience = false,
        ValidateIssuer = false
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
