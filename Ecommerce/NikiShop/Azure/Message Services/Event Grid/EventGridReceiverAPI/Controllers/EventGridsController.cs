using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using Azure.Messaging.EventGrid;

namespace EventGridSubscriberApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventGridsController : ControllerBase
    {
        private readonly ILogger<EventGridsController> _logger;
        public EventGridsController(ILogger<EventGridsController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> ReceiveEvent([FromBody] object events)
        {
            try
            {
                _logger.LogInformation("Inside ReceiveEvent Post");
                EventGridEvent[] eventGridEvents = EventGridEvent.ParseMany(new BinaryData(events));

                foreach (EventGridEvent eventGridEvent in eventGridEvents)
                {
                    _logger.LogInformation($"Event received. Event Type: {eventGridEvent.EventType}");

                    if (string.Equals(eventGridEvent.EventType,
                                    "EventType",
                                    StringComparison.OrdinalIgnoreCase))
                    {
                        _logger.LogInformation($"Handle system events: {eventGridEvent.Id} - {eventGridEvent.Data}");

                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing events");
                return StatusCode(500);
            } 
        }

        [HttpGet]
        public string Get()
        {
            return "Hola";
        }
    }
}
