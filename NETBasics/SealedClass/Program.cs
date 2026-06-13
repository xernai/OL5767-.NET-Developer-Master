using System;

namespace SealedClass
{
    public sealed class SAT: People
    {
       public void PagarISR()
       {
           Console.WriteLine("Pagar ISR");
       }
    }

    public class People
    {
        
    }

    internal class Program
    {
        static void Main(string[] args)
        {
           SAT sat = new SAT();
           sat.PagarISR();
        }
    }
}
