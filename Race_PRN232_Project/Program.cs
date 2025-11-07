using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Race_PRN232_Project.Models;
using Race_PRN232_Project.Helpers; // ch?a MappingProfile
using Race_PRN232_Project.Services.Interfaces;
using Race_PRN232_Project.Services.Implementations;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// =====================================
// 1?? Add Controllers + OData
// =====================================
builder.Services.AddControllers()
    .AddOData(opt => opt.EnableQueryFeatures());

// =====================================
// 2?? Swagger Config
// =====================================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "RacePRN232 API",
        Version = "v1",
        Description = "API for managing races, users, roles, and support teams."
    });

    // ?? JWT Definition
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Enter JWT token like: Bearer {token}",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

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
            new string[]{}
        }
    });
});

// =====================================
// 3?? Database Connection (SQL Server)
// =====================================
builder.Services.AddDbContext<RacePRN232Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyCnn"))
);

// =====================================
// 4?? JWT Authentication
// =====================================
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true, // ? Ki?m tra h?n token
        ClockSkew = TimeSpan.Zero, // ? Không cho phép l?ch th?i gian
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])
        )
    };
});


// =====================================
// 5?? Enable CORS
// =====================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// =====================================
// 6?? AutoMapper + Services
// =====================================
builder.Services.AddAutoMapper(typeof(MappingProfile));

// ??ng ký các service nghi?p v?
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRaceService, RaceService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ISupportTeamMemberService,SupportTeamMemberService>();
builder.Services.AddScoped<IRaceRegistrationService, RaceRegistrationService>();
builder.Services.AddScoped<ISupportTeamService, SupportTeamService>();

// =====================================
// 7?? Build App
// =====================================
var app = builder.Build();

// =====================================
// 8?? Configure Middleware
// =====================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
