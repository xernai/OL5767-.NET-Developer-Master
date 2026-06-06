using System;
using System.Collections.Generic;
using System.Linq;

namespace Module25
{
    delegate int Operator(int x, int y);

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // int resultado = Sumar(2, 3);

            Operator oper1 = new Operator(Sumar);

            oper1 = oper1 + delegate(int x, int y) { return x - y; };

            int resultado = oper1(1, 2);

            Console.WriteLine($"Resultado es: {resultado}");

            // Lambda expressions
            // 1. Expression lambda, es una sola línea de código
            // 2. Statement lambda, que son varias líneas de código

            //Func<int, int, int> multiplicacion1 = delegate (int x, int y) { return x  * y; };
            //Func<int, int, int> multiplicacion2 = (int x, int y) => { return x * y; };
            //Func<int, int, int> multiplicacion3 = (x, y) => { return x * y; };
            Func<int, int, int> multiplicacion4 = (x, y) => x * y;

            int resultado1 = multiplicacion4(1, 2);

            Func<int, int, int, int> multiplicacion5 = (x, y, z) => x * y;
            Action<int, int> action1 = (x, y) => {
                int suma = x + y;
                Console.WriteLine($"Suma es: {suma}");
            };

            // LINQ: Language INtegrated Query
            // CQRS

            // IEnumerable
            List<string> nombres = new List<string>();

            nombres.Add("Juan");
            nombres.Add("Maria");

            //int x = 1;
            // LINQ:
            // 1. Query
            // 2. Method
            var x = nombres.Where(x => x == "Juan").ToList();

            foreach(var item in x)
            {
                Console.WriteLine($"Nombre es: {item}");
            }
            

            // Query
            var y = (from nombre in nombres
                    where nombre == "Juan"
                    select nombre).ToList();

            Console.WriteLine($"Nombre es: {x}");

            // Select nombre
            // From nombres
            // Where nombre = 'Juan'

        }

        // x => x == "Juan"
        private static bool BuscarNombre(string nombre)
        {
            bool exito = false;
            
            if (nombre == "Juan")
            {
                exito = true;
            }
            
            return exito;
        }

        private static int Sumar(int v1, int v2)
        {
           return v1 + v2;
        }

        private static int Restar(int v1, int v2)
        {
            return v1 - v2;
        }
    }
}
