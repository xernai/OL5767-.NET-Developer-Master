using System;
using Utilities;

namespace CallBackExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();

            Func<int, int, int> del = calculator.Divide;

            EjecutaOperacion("División", del);

            // Otro metodo
        }

        private static void EjecutaOperacion(string nombre, Func<int, int, int> operacion)
        {
            if(nombre == "División")
            {
                int resultado = operacion(10, 5);

                Console.WriteLine($"El resultado de la division es:{resultado}");
            }

            // busca algo en internet
        }
    }
}
