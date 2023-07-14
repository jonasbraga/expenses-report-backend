// Question: Is it better to use global using statements or to use the using statement in each file?
// is there a difference between the two? or an standard to follow?
global using AccountOrganizationService.Services.DepartmentService;
global using AccountOrganizationService.Services.ProjectService;
global using AccountOrganizationService.Services.UserService;
global using Microsoft.AspNetCore.Authorization;
global using AccountOrganizationService.Models;
global using AccountOrganizationService.Data;
global using Microsoft.EntityFrameworkCore;
global using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

var env = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var envFile = (env == Environments.Production || env is null) ? ".env" : $".env.{env}";

DotNetEnv.Env.Load(envFile);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = System.Environment.GetEnvironmentVariable("CONNECTION_STRING");
    var serverVersion = new MySqlServerVersion(new Version(8, 0, 33));
    options.UseMySql(connectionString, serverVersion);
});
builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                    .GetBytes(System.Environment.GetEnvironmentVariable("JWT_TOKEN")!)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
