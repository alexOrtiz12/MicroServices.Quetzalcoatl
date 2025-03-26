namespace MicroServices.Quetzalcoatl.ActivosFijos.Extensions
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
        {
            var allowedOrigin = configuration["Cors:AllowedOrigin"];

            services.AddCors(options =>
            {
                options.AddPolicy("PermitirSoloCliente", policy =>
                    policy.WithOrigins(allowedOrigin)
                          .AllowAnyMethod()
                          .AllowAnyHeader());
            });

            return services;
        }
    }
}
