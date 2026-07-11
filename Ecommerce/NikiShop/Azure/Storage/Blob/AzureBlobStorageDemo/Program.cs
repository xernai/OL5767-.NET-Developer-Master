using Azure.Storage.Blobs;
using AzureBlobStorageDemo.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//var connection = builder.Configuration.GetConnectionString("AzureBlobStorageKey");
builder.Services.AddScoped(_ => {
    return new BlobServiceClient(builder.Configuration.GetConnectionString("AzureLocalStorageKey"));
});
builder.Services.AddScoped<IBlobStorage, BlobStorage>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
