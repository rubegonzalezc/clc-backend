using ApiCLC.Models;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Cargar variables de entorno desde el archivo .env
LoadEnvironmentVariables();

// Construir la cadena de conexión
var connectionString = BuildConnectionString(builder.Configuration);

// Configurar servicios
ConfigureServices(builder.Services, connectionString);

// Construir la aplicación
var app = builder.Build();

// Configurar el pipeline de la aplicación
ConfigureMiddleware(app);

app.Run();

void LoadEnvironmentVariables()
{
    Env.Load();
}

string BuildConnectionString(IConfiguration configuration)
{
    var dbUser = Environment.GetEnvironmentVariable("DB_USER");
    var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
    var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
    var dbPort = Environment.GetEnvironmentVariable("DB_PORT");

    if (string.IsNullOrEmpty(dbUser) || string.IsNullOrEmpty(dbPassword) || string.IsNullOrEmpty(dbHost))
    {
        throw new InvalidOperationException("Las variables de entorno para la base de datos no están configuradas correctamente.");
    }

    return $"User Id={dbUser};Password={dbPassword};Data Source={dbHost}:{dbPort}/xe";
}

void ConfigureServices(IServiceCollection services, string connectionString)
{
    services.AddDbContext<AppDbContext>(options =>
        options.UseOracle(connectionString));

    services.AddControllers();

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "CLCApi", Version = "v1" });
    });
}

void ConfigureMiddleware(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "CLCApi v1");
        });
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
}