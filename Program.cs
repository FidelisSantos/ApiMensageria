using ApiMensageria.Data;
using ApiMensageria.Interfaces;
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


builder.Services.AddDbContext<DataContext>(
    dbContextOptions => dbContextOptions
                .UseSqlite("DataSource=Mensageria.db;Cache=shared")
                // The following three options help with debugging, but should
                // be changed or removed for production.
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
);

builder.Services.AddAutoMapper(typeof(UserMapping));
builder.Services.AddScoped<ILoginServices, LoginServices>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IMessageServices, MessageServices>();


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
