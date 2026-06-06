using System;

namespace EventHandlerPassingValuesExample
{
    public class PrecautionsEventArgs : EventArgs
    {
        public string ToDo { get; set; }
    }

    // clase subscriptora
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

        public void NotifyTemperature(object sender, PrecautionsEventArgs e)
        {
            Console.WriteLine($"Notificando temperature del medio ambiente a {Name} y por favor toma estas medidas: {e.ToDo}");
        }
    }

    // clase publicadora
    public class WeatherForecast
    {
        public event EventHandler<PrecautionsEventArgs> OnTemperatureChanged;

        public void EvaluateTemperature(double temperature)
        {
            if (temperature == 35)
            {
                if (OnTemperatureChanged != null)
                {
                    OnTemperatureChanged(this, new PrecautionsEventArgs { ToDo = "Tomar agua y resguardarse del sol." });
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Example of EventHandler Passing Values.");
            Console.WriteLine("------------------------");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkBlue;

            People maia = new People();
            maia.Name = "Maia";

            People genaro = new People();
            genaro.Name = "Genaro";

            WeatherForecast weatherForecast = new WeatherForecast();

            weatherForecast.OnTemperatureChanged += maia.NotifyTemperature;
            weatherForecast.OnTemperatureChanged += genaro.NotifyTemperature;

            weatherForecast.EvaluateTemperature(35);

            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
