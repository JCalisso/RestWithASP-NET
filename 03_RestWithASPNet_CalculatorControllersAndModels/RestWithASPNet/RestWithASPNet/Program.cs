using Microsoft.EntityFrameworkCore;
using RestWithASPNet.Models.Context;
using RestWithASPNet.Business;
using RestWithASPNet.Business.Implementations;
using RestWithASPNet.Repository;
using RestWithASPNet.Repository.Implementations;
using RestWithASPNet.Repository.Generic;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add configuration builder 
IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();


// configure connection with database
var connection = config["db:connection"];
builder.Services.AddDbContext<SQLContext>(options => options.UseSqlServer(connection));

//Criação do logger
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();


// A partir do ASPNETCore 6, alguns serviços podem
// ser injetados diretamente na aplicação assim como
// o exemplo abaixo:
IWebHostEnvironment environment = builder.Environment;

if (environment.IsDevelopment())
{
    MigrateDatabase(connection);
}

// Add services to the container.
builder.Services.AddControllers();

//Verioning API
builder.Services.AddApiVersioning();
builder.Services.AddMvc();

//Dependency Injection
builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
builder.Services.AddScoped<IPersonRepository, PersonRepositoryImplementation>();

builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


void MigrateDatabase(string? connection)
{
    try
    {
        var evolveConnection = new Microsoft.Data.SqlClient.SqlConnection(connection);
        var evolve = new EvolveDb.Evolve(evolveConnection, msg => Log.Information(msg))
        {
            Locations = new List<string> { "db/migrations", "db/dataset"},
            IsEraseDisabled = true,
        };
        
        evolve.Migrate();        
    }
    catch (Exception ex)
    {
        Log.Error("Database migration failed", ex);
        throw;
    }
}
