using System;

namespace Module22
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int edad = 18;
            bool credito = false;

            try
            {
                int limiteCredito = int.MaxValue;

                limiteCredito = limiteCredito / 0;

                if (edad == 18)
                {
                    credito = true;
                    Console.WriteLine("Acceso a crédito");
                    ConsultarCredito();
                }
                else
                {
                    Console.WriteLine("No Acceso a crédito");
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine(ex.Message);
                Console.WriteLine("Error en calculo");
            }
            finally
            {
                Console.WriteLine("Estoy en finally");
            }  
        }

        private static void ConsultarCredito()
        {
            for(int i = 0; i < 12; i++)
            { 
                Console.WriteLine($"Mes {i + 1}");
            }

            // otra cosa despues del for
        }
    }
}
