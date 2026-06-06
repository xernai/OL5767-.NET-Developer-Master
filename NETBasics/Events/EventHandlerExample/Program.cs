using System;

namespace EventHandlerExample
{
    // 1. Se declara un delegado
    // ya no es necesario declarar un delegado
    // public delegate void TemperatureChangeHandler();

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

        public void NotifyTemperature(object x, EventArgs y)
        {
            Console.WriteLine($"Notificando temperature del medio ambiente a {Name} de la clase {x}");
        }
    }

    // Publisher
    public class WeatherForecast
    {
        // 2. El evento (keyword event es un modifier) se basa en el delegado EventHandler
        // Sugar syntax, Syntactic sugar
        public event EventHandler OnTemperatureChanged;

        public void EvaluateTemperature(double temperature)
        {
            if (temperature == 35)
            {
                if (OnTemperatureChanged != null)
                {
                    OnTemperatureChanged(this, EventArgs.Empty);
                }
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Example of Events with EventHandler.");
            Console.WriteLine("------------------------");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkBlue;

            People maia = new People();
            maia.Name = "Maia";

            People genaro = new People();
            genaro.Name = "Genaro";

            // Instancia la clase Publisher
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
