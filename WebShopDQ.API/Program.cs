using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebShopDQ.App.Data;
using WebShopDQ.App.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Entity Framework
builder.Services.AddDbContext<DatabaseContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? ""));

// Identity
builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<DatabaseContext>()
    .AddDefaultTokenProviders();

// Add Log Global
var _loggrer = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration)
                                        .Enrich.FromLogContext().CreateLogger();
builder.Logging.AddSerilog(_loggrer);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
