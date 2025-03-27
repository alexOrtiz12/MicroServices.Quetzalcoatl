using DotNetEnv;
using MicroServices.Quetzalcoatl.ActivosFijos.Data;
using MicroServices.Quetzalcoatl.ActivosFijos.Extensions;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);


Env.Load();

// Construir la cadena de conexi�n desde variables
var server = Environment.GetEnvironmentVariable("DB_SERVER") ??
    throw new InvalidOperationException("DB_SERVER no est� configurado.");
var port = Environment.GetEnvironmentVariable("DB_PORT") ?? "3306";
var database = Environment.GetEnvironmentVariable("DB_NAME") ??
    throw new InvalidOperationException("DB_NAME no est� configurado.");
var user = Environment.GetEnvironmentVariable("DB_USER") ??
    throw new InvalidOperationException("DB_USER no est� configurado.");
var password = Environment.GetEnvironmentVariable("DB_PASSWORD") ??
    throw new InvalidOperationException("DB_PASSWORD no est� configurado.");
var ssl = Environment.GetEnvironmentVariable("DB_SSL") ?? "Required";

// cadena de conexi�n
var connectionString = $"Server={server};Port={port};Database={database};User Id={user};Password={password};SslMode={ssl};";


builder.Services.AddDbContext<ActivoFijoDbContext>(options =>
    options.UseMySQL(
        builder.Configuration.GetConnectionString(connectionString)
    )
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuraci�n de CORS desde appsettings.json
builder.Services.AddCorsPolicy(builder.Configuration);

var app = builder.Build();

// Aplicar la pol�tica de CORS
app.UseCors("PermitirSoloCliente");

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();