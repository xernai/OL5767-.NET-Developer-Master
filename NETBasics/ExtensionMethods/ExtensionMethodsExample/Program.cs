using EventsExample;
using System;

namespace ExtensionMethods
{
    public static class PeopleExtended
    {
        public static string FullName(this People people)
        {
            return people.LastName.ToUpper() + " " + people.Name.ToUpper();
        }
    }

    public static class StringExtended
    {
        public static string FullName1(this string name)
        {
            return name.ToUpper();
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            People people = new People();
            people.Name = "Maia";
            people.LastName = "Perez";

            // Sin applicar el metodo extensor
            var fullName = people.LastName.ToUpper() + " " + people.Name.ToUpper();
            Console.WriteLine($"----------------------------------");
            Console.WriteLine($"Result from fullName {fullName}");

            // Aplicando el metodo extensor
            people.LastName = "Martínez";
            var fullNameExtended = people.FullName();

            Console.WriteLine($"------------ Usando fullname como extensor ----------------------");
            Console.WriteLine($"Result from fullNameExtended {fullNameExtended}");

            // Aplicando un metodo extensor al tipo de dato string
            string x = "Maia";

            var stringExtended = x.FullName1();
            Console.WriteLine($"----------------------------------");
            Console.WriteLine($"Result from stringExtended {stringExtended}");

        }
    }
}
