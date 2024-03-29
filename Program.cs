using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TaskManagerAPI.Data;
using TaskManagerAPI.Mappings;
using TaskManagerAPI.Repositories.Interface;
using TaskManagerAPI.Repositories.SQLServerImplementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TaskManagerDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("TaskManagerConnectionString")));

// Repositories
builder.Services.AddScoped<ITaskRepository, SQLServerTaskRepository>();
builder.Services.AddScoped<IListRepository, SQLServerListRepository>();

// Mapping
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

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
