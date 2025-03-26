using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MicroServices.Quetzalcoatl.Sucursales.Extensions
{
    public static class CorsExtension
    {
        public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
        {
            // Obtener el dominio permitido desde appsettings.json
            var allowedOrigin = configuration["Cors:AllowedOrigin"];

            services.AddCors(options =>
            {
                options.AddPolicy("PermitirSoloCliente", policy =>
                    policy.WithOrigins(allowedOrigin) // Se obtiene desde la configuración
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials()); // Permitir credenciales (si es necesario)
            });

            return services;
        }
    }
}