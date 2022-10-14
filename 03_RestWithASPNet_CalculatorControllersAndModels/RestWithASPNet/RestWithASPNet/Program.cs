using Microsoft.EntityFrameworkCore;
using RestWithASPNet.Models.Context;
using RestWithASPNet.Services;
using RestWithASPNet.Services.Implementations;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.


builder.Services.AddControllers();

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();



var connection = config["db:connection"];
builder.Services.AddDbContext<SQLContext>(options => options.UseSqlServer(connection));
builder.Services.AddMvc();
    
//Dependency Injection
builder.Services.AddScoped<IPersonService, PersonServiceImplementation>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();