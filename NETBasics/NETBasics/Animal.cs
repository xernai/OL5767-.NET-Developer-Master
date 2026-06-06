using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETBasics1
{
    public class Animal
    {
        public Animal() { }

        public Animal(int edad) { }

        public string Name { get; set; }

        private decimal Costo { get; set; }
        public void Comer()
        {
            Console.WriteLine("Como");
        }

        public void Correr()
        {
            Console.WriteLine("Corro");
        }

        public void Cantar()
        {
            Console.WriteLine("Trino");
        }

        public void Cantar(string nombre)
        {
            Console.WriteLine("Gruño");
        }
    }
}
