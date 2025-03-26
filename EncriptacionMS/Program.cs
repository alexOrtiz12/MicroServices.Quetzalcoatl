using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using System;

class Program
{
    static void Main()
    {
        var services = new ServiceCollection();
        services.AddDataProtection();
        var provider = services.BuildServiceProvider();
        var protector = provider.GetService<IDataProtectionProvider>().CreateProtector("CifradoSucursales");

        string original = "Server=sql.freedb.tech;Port=3306;Database=freedb_TrQuetz;User Id=freedb_brayam;Password=8Z&DK2TVBW2MPfa;SslMode=Required;";
        string encrypted = protector.Protect(original);

        Console.WriteLine($"Cadena cifrada: {encrypted}");
    }
}
