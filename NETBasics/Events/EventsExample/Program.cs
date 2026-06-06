using System;

namespace EventsExample
{
    // 1. Se declara un delegado
    public delegate void TemperatureChangeHandler();

    // Clase subscriptora
    public class People
    {
        private string name;

        public string Name
        {
            get { return name; }

            set
            {
                name = value;
            }
        }

        public string LastName { get; set; }
        public int Age { get; set; }

        public void NotifyTemperature()
        {
            Console.WriteLine($"Notificando temperature del medio ambiente a {Name}");
        }
    }

    // Publisher
    public class WeatherForecast
    {
        // 2. El evento (keyword event es un modifier) se basa en el delegado TemperatureChangeHandler
        // syntax sugar, Syntactic sugar
        public event TemperatureChangeHandler OnTemperatureChanged;

        public void EvaluateTemperature(double temperature)
        {
            if (temperature == 35)
            {
                if (OnTemperatureChanged != null)
                {
                    OnTemperatureChanged();
                }
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Example of Events.");
            Console.WriteLine("------------------------");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkBlue;

            People maia = new People();
            maia.Name = "Maia";

            People genaro = new People();
            genaro.Name = "Genaro";

            //  la clase InstanciaPublisher
            WeatherForecast weatherForecast = new WeatherForecast();

            // Subscribo los event handlers, no son más que los métodos que el evento va a ejecutar
            weatherForecast.OnTemperatureChanged += maia.NotifyTemperature;
            weatherForecast.OnTemperatureChanged += genaro.NotifyTemperature;

            weatherForecast.EvaluateTemperature(35);

            weatherForecast.OnTemperatureChanged -= maia.NotifyTemperature;
            weatherForecast.OnTemperatureChanged -= genaro.NotifyTemperature;

            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}

