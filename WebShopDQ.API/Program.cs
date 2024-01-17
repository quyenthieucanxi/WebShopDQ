using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebShopDQ.App.AutoMapper;
using WebShopDQ.App.Common;
using WebShopDQ.App.Data;
using WebShopDQ.App.Dependency;
using WebShopDQ.App.Models;
using Serilog;
using WebShopDQ.App.Repositories;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.Services;
using WebShopDQ.App.Services.IServices;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddCors()
        .AddRepository()
        .AddServices()
        .AddAutoMapper(typeof(AutoMapperProfile).Assembly);
}

// Add auto mapper
builder.Services.AddAutoMapper(typeof(Program));


// Entity Framework
builder.Services.AddDbContext<DatabaseContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? ""));

// Identity
builder.Services.AddIdentity<User, Role>(options =>
{
    // Configure password checking
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
})
    .AddEntityFrameworkStores<DatabaseContext>()
    .AddDefaultTokenProviders();


// Add Config for required Email
builder.Services.Configure<IdentityOptions>(opts => opts.SignIn.RequireConfirmedEmail = true);

// Add Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        //        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        //        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"])),

        ClockSkew = TimeSpan.Zero
    };
});

// Add Email Configs
/*var emailConfig = configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IEmailService, EmailService>();*/

// Add Cors\
builder.Services.AddCors(options => options.AddPolicy("corpolicyHttp",policy =>
{
    policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}
));
builder.Services.AddCors(options => options.AddPolicy("corpolicyHttps", policy =>
{
    policy.WithOrigins("https://fe-web-shop-dq.vercel.app").AllowAnyMethod().AllowAnyHeader();
}
));
// Add IUrlHelper
builder.Services.AddScoped<IUrlHelper>(x =>
{
    var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
    var factory = x.GetRequiredService<IUrlHelperFactory>();
    return factory.GetUrlHelper(actionContext!);
});
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

// Add Log Global
var _loggrer = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration)
                                        .Enrich.FromLogContext().CreateLogger();
builder.Logging.AddSerilog(_loggrer);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});



// Cloundinary

builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});

Account account = new(
    builder.Configuration["Cloudinary:CloudName"],
    builder.Configuration["Cloudinary:ApiKey"],
    builder.Configuration["Cloudinary:ApiSecret"]);
Cloudinary cloudinary = new(account);

// Login check
builder.Services.Configure<IdentityOptions>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;
});

var app = builder.Build();
app.UseCors("corpolicyHttp");
app.UseCors("corpolicyHttps");
app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();
