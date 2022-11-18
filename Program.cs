using ApiMensageria.Data;
using ApiMensageria.Interfaces;
using ApiMensageria.Repository;
using ApiMensageria.Mapping;
using ApiMensageria.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
        .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionStringMysql = builder.Configuration.GetConnectionString("ConnectionMysql");
var connectionStringPlanetScale = builder.Configuration.GetConnectionString("ConnectionPlanetScale");
var connectionSqLite = builder.Configuration.GetConnectionString("ConnectionSqLite");

//A extensão AddDbContext por padrão é injetada com um Singleton criando uma instância só para a aplicação inteira.
builder.Services.AddDbContext<DataContext>(
    dbContextOptions => dbContextOptions
                .UseMySql(
                  connectionStringPlanetScale,
                  ServerVersion.Parse("mysqld 5.7.39")
                )
                .LogTo(Console.WriteLine, LogLevel.Information)
);

builder.Services.AddAutoMapper(typeof(UserMapping));
builder.Services.AddScoped<ILoginServices, LoginServices>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IMessageServices, MessageServices>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
