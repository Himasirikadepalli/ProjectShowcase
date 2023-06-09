using DataAccessLayer.Data;
using BussinessLogicLayer.DTOs;
using BussinessLogicLayer.Services;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Models.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//***************Logging***************

builder.Logging.ClearProviders();

builder.Logging.AddLog4Net();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
 option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
 {
     Name = "Authorization",
     Type = SecuritySchemeType.ApiKey,
     Scheme = "Bearer",
     BearerFormat = "JWT",
     In = ParameterLocation.Header,
     Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
 }));
builder.Services.AddSwaggerGen(option =>
 option.AddSecurityRequirement(new OpenApiSecurityRequirement
 {
 {
 new OpenApiSecurityScheme
 {
 Reference = new OpenApiReference
 {
 Type = ReferenceType.SecurityScheme,
 Id = "Bearer"
 }
 },
 new string[] {}
 }
 }
 ));


//authentication
builder.Services.AddIdentity<MyUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
 .AddDefaultTokenProviders()
 .AddEntityFrameworkStores<AuthDbContext>();



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        //ValidIssuer = builder.Configuration["Jwt:Issuer"],
        //ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
    (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

// Database
string connectionString = builder.Configuration.GetConnectionString("Conn");
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(connectionString, c => c.MigrationsAssembly("WebApi"))
);
builder.Services.AddDbContext<AuthDbContext>(
    options => options.UseSqlServer(connectionString, c => c.MigrationsAssembly("WebApi"))
);

// DI
builder.Services.AddScoped<IProjectRepositories, ProjectRepositories>();
builder.Services.AddScoped<ITicketRepositories, TicketRepositories>();
builder.Services.AddScoped<ITicketServices, TicketServices>();
builder.Services.AddScoped<IProjectServices, ProjectServices>();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors(MyAllowSpecificOrigins);


app.MapControllers();

app.Run();
