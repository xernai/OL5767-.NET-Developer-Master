using Azure.Data.Tables;
using Azure.Storage.Blobs;

var builder = WebApplication.CreateBuilder(args);

// Obtener la cadena de conexión
string connectionString = builder.Configuration.GetConnectionString("AzureStorage");

// Registrar Clientes de Azure Storage como Singletons
builder.Services.AddSingleton(new TableServiceClient(connectionString));
builder.Services.AddSingleton(new BlobServiceClient(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
