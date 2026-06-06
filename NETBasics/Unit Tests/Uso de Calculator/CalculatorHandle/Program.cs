using System;
using Utilities;

namespace CalculatorHandle
{
    internal class Program
    {
        static void Main(string[] args)
        {
           Calculator calculator = new Calculator();

           var resultado = calculator.Divide(4, 2);

           Console.WriteLine(resultado);
        }
    }
}
