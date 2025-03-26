using MicroServices.Quetzalcoatl.Proveedores.Data;
using MicroServices.Quetzalcoatl.Proveedores.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuraci�n de MySQL con la cadena de conexi�n desde appsettings.json
builder.Services.AddDbContext<ProveedorDbContext>(options =>
    options.UseMySQL(
        builder.Configuration.GetConnectionString("DefaultConnection")
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