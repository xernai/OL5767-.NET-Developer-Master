using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace TopicsFiltersAndActionsWebApi.Controllers
{
    public class Customer
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public List<string> Phone { get; set; }
        public int Address { get; set; }
        public string City { get; set; }
    }

    public class Client
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public decimal Credit { get; set; }
    }

    public class Order
    {
        public int OrderID { get; set; }
        public List<string> Product { get; set; }
        public string Phone { get; set; }
    }

    public class OrderDetail
    {
        public int OrderID { get; set; }
    }

        [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        //[HttpPost]
        //public async Task Post(WeatherForecast data)
        //{
        //    // Assumes we write this to a database
        //    var message = new WeatherForecastAdded()
        //    {
        //        Id = Guid.NewGuid(),
        //        CreatedDateTime = DateTime.UtcNow,
        //        ForDate = data.Date
        //    };

        //    var connectionString = "<YOUR CONNECTION STRING>";

        //    var client = new ServiceBusClient(connectionString);
        //    var sender = client.CreateSender("weather-forecast-added");

        //    var body = JsonSerializer.Serialize(message);
        //    var sbMessage = new ServiceBusMessage(body);

        //    // TODO: si los clientes son de Leon, mandales la oferta si el mes es diciembre
        //    sbMessage.ApplicationProperties.Add("Month", data.Date.ToString("MMMM"));
        //    await sender.SendMessageAsync(sbMessage);

        //    // Create new subscription
        //    ServiceBusAdministrationClient administrationClient = new ServiceBusAdministrationClient(connectionString);

        //    await administrationClient.CreateSubscriptionAsync(
        //        new CreateSubscriptionOptions("yourtopic", "SqlFilterAndActionSubscription"),
        //        new CreateRuleOptions
        //        {
        //            Action = new SqlRuleAction("SET Age = Age * 2;"),
        //            Filter = new SqlRuleFilter("Name = 'Azure'"),
        //            Name = "SqlFilterAndAction"
        //        }
        //    );

        //    var customer = new Customer
        //    {
        //        Age = 20,
        //        Name = "Azure"
        //    };

        //    var sender1 = client.CreateSender("yourtopic");

        //    var serviceBusMessage = new ServiceBusMessage()
        //    {
        //        ApplicationProperties = { { "Name", customer.Name }, { "Age", customer.Age } }
        //    };

        //    await sender1.SendMessageAsync(serviceBusMessage);

        //    // Update Subscription
        //    var createRuleOptions = new CreateRuleOptions
        //    {
        //        Action = new SqlRuleAction("SET Age = Age * 2;"),
        //        Filter = new SqlRuleFilter("Name='Azure'"),
        //        Name = "SqlFilterAndAction"
        //    };
        //    await administrationClient.CreateRuleAsync("weather-forecast-added", "update-report", createRuleOptions);

        //    await sender.SendMessageAsync(serviceBusMessage);
        //}

        [HttpPost]
        public async Task Post()
        {
            var connectionString = "";
            var client = new ServiceBusClient(connectionString);

            // Create new subscription
            ServiceBusAdministrationClient administrationClient = 
                new ServiceBusAdministrationClient(connectionString);

            await administrationClient.CreateSubscriptionAsync(
                new CreateSubscriptionOptions("rustico", "SqlFilterAndActionSubscription-3"),
                new CreateRuleOptions
                {
                    Action = new SqlRuleAction("SET Age = Age * 2;"),
                    Filter = new SqlRuleFilter("Name = 'AWS'"),
                    Name = "SqlFilterAndAction"
                }
            );

            var customer = new Customer
            {
                Age = 20,
                Name = "AWS"
            };

            var sender1 = client.CreateSender("rustico");

            var serviceBusMessage = new ServiceBusMessage()
            {
                ApplicationProperties = { { "Name", customer.Name }, { "Age", customer.Age } }
            };

            await sender1.SendMessageAsync(serviceBusMessage);
        }

        // [HttpPost]
        //public async Task Post()
        //{
        //    // Assumes we write this to a database
          
        //}
    }
}
