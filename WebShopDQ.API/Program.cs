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
using WebShopDQ.App.Interceptor;
using WebShopDQ.App.Hubs;
using Hangfire;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

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
builder.Services.AddDbContext<DatabaseContext>(
    (sp,options) =>
    {
        var auditableInterceptor = sp.GetService<UpdateAuditableEntitiesInterceptor>()!;
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? "")
                .AddInterceptors(auditableInterceptor);
    });

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
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"])),

        ClockSkew = TimeSpan.Zero
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];

            // If the request is for our hub...
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) &&
                (path.StartsWithSegments("/chat")))
            {
                // Read the token out of the query string
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});

// Add Email Configs
/*var emailConfig = configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IEmailService, EmailService>();*/

// Add Cors\
builder.Services.AddCors(options => options.AddPolicy("corpolicyHttp",policy =>
{
    policy.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
}
));
builder.Services.AddCors(options => options.AddPolicy("corpolicyHttps", policy =>
{
    policy.WithOrigins("https://fe-web-shop-dq.vercel.app").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
}
));
// Add IUrlHelper
builder.Services.AddScoped<IUrlHelper>(options =>
{
    var actionContext = options.GetRequiredService<IActionContextAccessor>().ActionContext;
    var factory = options.GetRequiredService<IUrlHelperFactory>();
    return factory.GetUrlHelper(actionContext!);
});
builder.Services.AddHangfire((sp, config) =>
{
    var connectionString = sp.GetRequiredService<IConfiguration>().GetConnectionString("DefaultConnection");
    config.UseSqlServerStorage(connectionString);
});
builder.Services.AddHangfireServer();
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

// Add Log Global
var _loggrer = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration)
                                        .Enrich.FromLogContext().CreateLogger();
builder.Logging.AddSerilog(_loggrer);
//Add SingalIR
builder.Services.AddSignalR();
// Add services to the container.
builder.Services.AddSingleton<UpdateAuditableEntitiesInterceptor>();
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
builder.Services.AddHttpContextAccessor();
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
app.UseHangfireDashboard();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();
app.MapHub<ChatHub>("/chat");

app.UseCors("corpolicyHttp");
app.UseCors("corpolicyHttps");
app.Run();
