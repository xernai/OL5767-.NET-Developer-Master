using System;

namespace Generics02
{
    public class Box<T>
    {
        public T Content { get; private set; }

        public Box(T content)
        {
            Content = content;
        }
    }

    public class Athlete
    {
        public string Name { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
    }

    public class FootballPlayer : Athlete
    {
        public string Position { get; set; }
        public int Goals { get; set; }
    }

    public class BaseballPlayer : Athlete
    {
        public string Position { get; set; }
        public int GamesStarted { get; set; }
    }

    public class TennisPlayer : Athlete
    {
        public string Position { get; set; }
        public int Points { get; set; }
    }

    //public class FootballTeam
    //{
    //    public string CoachName { get; set; }
    //    public FootballPlayer[] Members { get; set; }

    //    public FootballTeam(int maxMembers)
    //    {
    //        Members = new FootballPlayer[maxMembers];
    //    }
    //}

    //public class TennisTeam
    //{
    //    public string CoachName { get; set; }
    //    public TennisPlayer[] Members { get; set; }
    //    public TennisTeam(int maxMembers)
    //    {
    //        Members = new TennisPlayer[maxMembers];
    //    }
    //}

    //public class BaseballTeam
    //{
    //    public string CoachName { get; set; }
    //    public BaseballPlayer[] Members { get; set; }
    //    public BaseballTeam(int maxMembers)
    //    {
    //        Members = new BaseballPlayer[maxMembers];
    //    }
    //}

    public class Team<T> where T : Athlete
    {
        public string CoachName { get; set; }
        public T[] Members { get; set; }

        public Team(int maxMembers)
        {
            Members = new T[maxMembers];
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var cajaDeEntero = new Box<int>(5);
            var cajaDeString = new Box<string>("Hola mundo 1");

            Console.WriteLine(cajaDeEntero.Content); // 5
            Console.WriteLine(cajaDeString.Content); // Hola mundo 1

            // También se pueden anidar declaraciones en los tipos genéricos:
            var cajaDeCajas = new Box<Box<string>>(cajaDeString);
            Console.WriteLine(cajaDeCajas); // [Box: Content=[Box: Content=Hola mundo]]
            Console.WriteLine(cajaDeCajas.Content.Content); // [Box: Content=[Box: Content=Hola mundo]]

            //
            // instanciar Athlete
            Athlete athlete = new Athlete();
            athlete.Name = "Karla";

            Athlete athlete1 = new Athlete { Name = "Juan" };

            //// Sin generics
            //var vitesse = new FootballTeam(11);

            //vitesse.Members[0] = new FootballPlayer { Name = "Alex Renato Ibarra Mina" };
            //vitesse.Members[1] = new FootballPlayer { Name = "Pedro" };
            //// ..
            //vitesse.Members[10] = new FootballPlayer { Name = "jose" };

            //var losAngeles = new BaseballTeam(25);
            //losAngeles.Members[0] = new BaseballPlayer { Name = "Julio César Urías" };

            // maximizar la reutilización de código, podríamos crear una clase genérica, digamos Team<T>
            var vitesse1 = new Team<FootballPlayer>(11);
            vitesse1.Members[0] = new FootballPlayer { Name = "Alex Renato Ibarra Mina", 
                Height = 1.80, Weight = 90, Goals = 10, Position = "Delantero" };

            var losAngeles1 = new Team<BaseballPlayer>(25);
            losAngeles1.Members[0] = new BaseballPlayer { Name = "Julio César Urías" };
            losAngeles1.Members[1] = new BaseballPlayer { Name = "Valenzuela", Position = "Pelotero" };

            Console.WriteLine(losAngeles1.Members[0].Name);
        }
    }
}
