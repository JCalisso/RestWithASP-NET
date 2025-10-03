using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Rewrite;

using RestWithASPNet.Models.Context;
using RestWithASPNet.Business;
using RestWithASPNet.Business.Implementations;
using RestWithASPNet.Repository;
using RestWithASPNet.Repository.Generic;
using RestWithASPNet.Hypermedia.Filters;
using RestWithASPNet.Hypermedia.Enricher;
using RestWithASPNet.Services;
using RestWithASPNet.Services.Implementations;
using RestWithASPNet.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection.Extensions;


var builder = WebApplication.CreateBuilder(args);

var appName = "API RESTful developed in course 'REST API's from 0 to Azure with ASP.NET Core 8 and Docker'";
var appVersion = "v1";
var appDescription = $"REST API RESTful developed in course '{appName}'";

DotNetEnv.Env.Load("conn.env");

// Add configuration builder 
IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();


// configure connection with database
//var connection = config["db:connection"];

var connection = Environment.GetEnvironmentVariable("DB_CONNECTION");
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

// Desabilitado pois agora é executado ao subir a Imagem do Docker
// if (environment.IsDevelopment())
// {
//      MigrateDatabase(connection);
// }


// Add services to the container.
builder.Services.AddRouting(options => options.LowercaseUrls = true); //configura as letras min�sculas na URL


//var tokenConfigurations = new TokenConfiguration();
var tokenConfigurations = new TokenConfiguration
{
    Audience = Environment.GetEnvironmentVariable("TOKEN_AUDIENCE") ?? "",
    Issuer = Environment.GetEnvironmentVariable("TOKEN_ISSUER") ?? ""   ,
    Secret = Environment.GetEnvironmentVariable("TOKEN_SECRET") ?? "",
    Minutes = int.Parse(Environment.GetEnvironmentVariable("TOKEN_MINUTES") ?? "60"),
    DaysToExpire = int.Parse(Environment.GetEnvironmentVariable("TOKEN_DAYS_TO_EXPIRE") ?? "7")
};


// new ConfigureFromConfigurationOptions<TokenConfiguration>(
//     //builder.Configuration.GetSection("TokenConfigurations")
//     builder.Configuration.GetSection("TokenConfigurations")
// ).Configure(tokenConfigurations);

builder.Services.AddSingleton(tokenConfigurations);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = tokenConfigurations.Issuer,
        ValidAudience = tokenConfigurations.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.Secret))
    };
});

builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser().Build());
});

builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
}));
builder.Services.AddControllers();

var filterOptions = new HyperMediaFilterOptions();
filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
filterOptions.ContentResponseEnricherList.Add(new BookEnricher());

builder.Services.AddSingleton(filterOptions);

//Verioning API
builder.Services.AddApiVersioning();
builder.Services.AddMvc();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c =>
{
    c.SwaggerDoc(appVersion,
        new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = appName,
            Version = appVersion,
            Description = appDescription,
            Contact = new Microsoft.OpenApi.Models.OpenApiContact
            {
                Name = "Jean Calisso",
                Url = new Uri("https://github.com/JCalisso")
            },
        });
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Entre com o token JWT",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",

    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
}
);

//Dependency Injection
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>();
builder.Services.AddScoped<ILoginBusiness, LoginBusinessImplementation>();
builder.Services.AddScoped<IFileBusiness, FileBusinessImplementation>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

builder.Services.AddTransient<ITokenService, TokenService>();


builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapControllerRoute("DefaultApi", "{controller=values}/{id?}");
});

app.UseCors();

app.UseSwagger();
app.UseSwaggerUI( c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{appName} - {appVersion}");
});

var option = new RewriteOptions();
option.AddRedirect("^$", "swagger");
app.UseRewriter(option);

//app.MapControllers();
//app.MapControllerRoute("DefaultApi", "{controller=values}/{id?}");

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
