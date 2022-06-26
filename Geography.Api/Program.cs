using AutoMapper;
using Dapper.EntityFramework;
using Geography.Api.Mapper;
using Geography.BL.Services;
using Geography.BL.Services.Abstract;
using Geography.DAL.Repositories;
using Geography.DAL.Repositories.Abstract;
using FluentValidation.AspNetCore;
using Geography.Api.Extensions;
using FluentMigrator.Runner;
using System.Reflection;
using Geography.Api.Migrations;

Handlers.Register();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

builder.Services
        .AddFluentMigratorCore()
        .ConfigureRunner(c => c
            .AddSqlServer2016()
            .WithGlobalConnectionString("DefaultConnection")
            .ScanIn(Assembly.GetExecutingAssembly()).For.All());

builder.Services.AddControllers()
    .AddFluentValidation();
builder.Services.AddSingleton<IDemoSnapshotRepository, DemoSnapshotRepository>();
builder.Services.AddSingleton<ISqlServerConnectionProvider>(
    new SqlServerConnectionProvider(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSingleton<IDemoSnapshotService, DemoSnapshotService>();
builder.Services.AddSingleton<IMapper>(service => new Mapper(MapperConfig.GetConfiguration()));
builder.Services.AddValidators();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Migrate();

app.Run();