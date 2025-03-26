using MicroServices.Quetzalcoatl.ActivosFijos.Data;
using MicroServices.Quetzalcoatl.ActivosFijos.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuración de MySQL con la cadena de conexión desde appsettings.json
builder.Services.AddDbContext<ActivoFijoDbContext>(options =>
    options.UseMySQL(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de CORS desde appsettings.json
builder.Services.AddCorsPolicy(builder.Configuration);

var app = builder.Build();

// Aplicar la política de CORS
app.UseCors("PermitirSoloCliente");

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();