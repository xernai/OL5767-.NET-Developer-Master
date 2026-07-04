using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NikiShop.Ecommerce.WebApi;
using System;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                // 1. Construir la configuración interna temporal para leer la URI
                var builtConfig = config.Build();
                string keyVaultUri = builtConfig["AzureKeyVault:Uri"];

                if (!string.IsNullOrEmpty(keyVaultUri))
                {
                    // 2. Añadir Azure Key Vault usando las credenciales por defecto
                    config.AddAzureKeyVault(
                        new Uri(keyVaultUri),
                        new DefaultAzureCredential()
                    );
                }
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}