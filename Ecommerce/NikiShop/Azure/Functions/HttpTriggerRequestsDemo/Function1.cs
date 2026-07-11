using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace HttpTriggerRequestsDemo;

public class Function1
{
    private readonly ILogger<Function1> _logger;

    public Function1(ILogger<Function1> logger)
    {
        _logger = logger;
    }

    [Function("Function1")]
    public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        if (req.Method == "POST")
        {
            _logger.LogInformation("Tipo Post.");

            using (var reader = new StreamReader(req.Body))
            {
                var body = await reader.ReadToEndAsync();
                _logger.LogInformation(body);

                try
                {
                    var obj = JObject.Parse(body);
                    foreach (var prop in obj.Properties())
                    {
                        _logger.LogInformation($"{prop.Name}: {prop.Value}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning($"Failed to parse JSON body: {ex.Message}");
                }
            }
        }

        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
    }
}