using NETBasics1;
using System;

namespace NETBasics
{
    class Vertebrado
    {

    }

    interface Vuelo
    {
        void Volar();
    }

    class Oso : Animal
    {
   
        public Oso(int edad)
        {


        }
        public int Nombre { get; set; }
    }

    class Pajaro : Animal, Vuelo
    {
        public void Volar()
        {
            Console.WriteLine("Vuelo");
        }
    }

    internal class Program
    {
        // OOP
        // Abstraction
        // Encapsulation
        // Inheritance
        // Polimorphism

        // Tipos de datos
        // 1. Tipos por valor: int
        //     1.1 Los valores se guardan en la memoria llamada stack
        // 2. Tipos por referencia: string, class, interface
        //    2.1 Se guarda la dirección de memoria de la llamada memoria heap en la memoria stack
        //    2.2 Los datos que debe almacenar la variable se guardan en la memoria heap

        static void Main(string[] args)
        {
            int edad = 20;
            edad = 30;

            string nombre = "Juan";

            Console.WriteLine($"Mi nombre es {nombre} y mi edad es: {edad}");

            // ------- Instancia de la clase animal
            //Animal animal = new Animal();
            //animal.Comer();

            //animal.Correr();

            Oso oso = new Oso(1);
            oso.Comer();
            oso.Correr();
            oso.Cantar("oso");

            // -- pajaro --
            Pajaro pajaro = new Pajaro();
            pajaro.Comer();
            pajaro.Correr();
            pajaro.Volar();
            pajaro.Cantar();

            Leer();

            string rfc = "xxxx";

            // Como pueden ver las interfaces no se pueden instanciar
            // Vuelo vuelo = new Vuelo();

        }

        private static void Leer()
        {
            int edad = 20;

            Oso oso = new Oso(1);
        }
    }
}
