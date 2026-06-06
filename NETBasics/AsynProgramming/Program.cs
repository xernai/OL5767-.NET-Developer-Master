using System;
using System.Threading.Tasks;

namespace AsynProgramming
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("🍽️  El mesero inicia su turno.\n");

            // 1. El mesero toma la orden de la Mesa 1 y la manda a la cocina
            // Notación: Arrancamos la tarea pero NO la esperamos de inmediato
            Task<string> ordenMesa1 = CocinarPlatilloAsync("Mesa 1", "Corte de Carne", 40000); // Tarda 4 segundos

            // 2. ¡Aquí está la magia asíncrona! 
            // Mientras la cocina trabaja en la Mesa 1, el mesero NO se queda congelado.
            Console.WriteLine("🏃 Mesero: Mientras se cocina lo de la Mesa 1, voy a atender a la Mesa 2...");
            await Task.Delay(1500); // Simula el tiempo que toma tomar otra orden o servir agua
            Console.WriteLine("🍷 Mesero: ¡Listo! Ya serví las bebidas en la Mesa 2.");

            Console.WriteLine("\n⏳ Mesero: Regreso al mostrador a esperar que la cocina me avise...");

            // 3. Ahora el mesero espera activamente a que la cocina termine el platillo de la Mesa 1
            string platilloListo = await ordenMesa1;

            // 4. El platillo está listo y el mesero lo entrega
            Console.WriteLine($"\n🔔 {platilloListo}");
            Console.WriteLine("🏃 Mesero: Llevando el platillo caliente a la Mesa 1.");
            Console.WriteLine("🎉 ¡Servicio completado con éxito!");
        }

        // Este método representa a la cocina (operación asíncrona)
        static async Task<string> CocinarPlatilloAsync(string mesa, string platillo, int tiempoCoccion)
        {
            Console.WriteLine($"🍳 [Cocina] Empezando a preparar: {platillo} para la {mesa}...");

            // El chef se pone a cocinar. Task.Delay simula el tiempo de cocción sin bloquear al mesero.
            await Task.Delay(tiempoCoccion);

            return $"[Cocina] ¡{platillo} de la {mesa} terminado!";
        }
    }
}
