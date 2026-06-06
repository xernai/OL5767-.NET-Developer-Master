using System;
using System.Collections.Generic;

namespace Module24
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> alumnos = new List<int> { 1, 2, 3, 4, 5 };

            for (int i = 0; i < alumnos.Count; i++)
            {
                Console.WriteLine(alumnos[i]);
            }

            List<string> nombres = new List<string>();

            nombres.Add("Juan");
            nombres.Add("Maria");

            for (int i = 0; i < nombres.Count; i++)
            {
                Console.WriteLine(nombres[i]);
            }

            int edad = 0;
            int[] notas = new int[3];
            int[] notas1 = { 1, 3, 4};

            // Syntax sugar

            notas[0] = 10;
            notas[1] = 11;
            notas[2] = 12;
            // notas[3] = 15;

            Console.WriteLine(notas[1]);

        }
    }
}
