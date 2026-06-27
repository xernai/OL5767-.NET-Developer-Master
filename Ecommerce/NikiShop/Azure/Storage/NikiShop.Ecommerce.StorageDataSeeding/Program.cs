using System;
using System.Threading.Tasks;

// Informar al usuario el inicio del proceso
Console.WriteLine("=========================================================");
Console.WriteLine("   Inicializador de Infraestructura Local Azure Storage  ");
Console.WriteLine("=========================================================\n");

try
{
    // Asegúrate de tener levantado tu contenedor de Azurite en Docker antes de ejecutar esto
    Console.WriteLine("[INFO] Conectando al emulador local (Azurite)...");

    // Llamada asíncrona al método estático de inicialización y seeding
    await LocalStorageInitializer.InicializarYAlimentarEstructuraLocalAsync();

    Console.WriteLine("\n[ÉXITO] Todo el entorno se configuró correctamente.");
    Console.WriteLine("[INFO] Ya puedes abrir Azure Storage Explorer para validar los datos.");
}
catch (Azure.RequestFailedException ex) when (ex.Status == 0)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("\n[ERROR] No se pudo establecer conexión con Azurite.");
    Console.WriteLine("[AYUDA] Verifica que el contenedor de Docker esté encendido y mapeando los puertos correspondientes.");
    Console.ResetColor();
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"\n[ERROR INESPERADO] Ocurrió un fallo: {ex.Message}");
    Console.ResetColor();
}

Console.WriteLine("\nPresiona cualquier tecla para salir...");
Console.ReadKey();
