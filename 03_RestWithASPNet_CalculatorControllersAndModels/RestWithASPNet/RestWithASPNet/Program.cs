using Microsoft.EntityFrameworkCore;
using RestWithASPNet.Models.Context;
using RestWithASPNet.Business;
using RestWithASPNet.Business.Implementations;
using RestWithASPNet.Repository;
using RestWithASPNet.Repository.Generic;
using Serilog;
using Microsoft.Net.Http.Headers;
using RestWithASPNet.Hypermedia.Filters;
using RestWithASPNet.Hypermedia.Enricher;
using Microsoft.AspNetCore.Rewrite;

var builder = WebApplication.CreateBuilder(args);

// Add configuration builder 
IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();


// configure connection with database
var connection = config["db:connection"];
builder.Services.AddDbContext<SQLContext>(options => options.UseSqlServer(connection));

//Cria��o do logger
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();


// A partir do ASPNETCore 6, alguns servi�os podem
// ser injetados diretamente na aplica��o assim como
// o exemplo abaixo:
IWebHostEnvironment environment = builder.Environment;

builder.Services.AddMvc(options =>
{
    options.RespectBrowserAcceptHeader = true;

    options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));

    options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));


})
.AddXmlSerializerFormatters();

if (environment.IsDevelopment())
{
    MigrateDatabase(connection);
}

// Add services to the container.
builder.Services.AddControllers();

var filterOptions = new HyperMediaFilterOptions();
filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
filterOptions.ContentResponseEnricherList.Add(new BookEnricher());

builder.Services.AddSingleton(filterOptions);

//Verioning API
builder.Services.AddApiVersioning();
builder.Services.AddMvc();

builder.Services.AddSwaggerGen( c =>
{
    c.SwaggerDoc("v1",
        new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "REST API's from 0 to Azure with ASP.NET Core 5 and Docker",
            Version = "v1",
            Description = "API RESTful developed in course 'REST API's from 0 to Azure with ASP.NET Core 5 and Docker'",
            Contact = new Microsoft.OpenApi.Models.OpenApiContact
            {
                Name = "Jean Calisso",
                Url = new Uri("https://github.com/JCalisso")
            }
        });
});

//Dependency Injection
builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI( c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json",
        "REST API's from 0 to Azure with ASP.NET Core 5 and Docker - v1");
});

var option = new RewriteOptions();
option.AddRedirect("^$", "swagger");
app.UseRewriter(option);

app.MapControllers();
app.MapControllerRoute("DefaultApi", "{controller=values}/{id?}");

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
