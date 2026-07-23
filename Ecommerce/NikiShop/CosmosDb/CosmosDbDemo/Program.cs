using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.Json;

string connectionString = "";

//// Crear el cliente de MongoDB estándar
//var client = new MongoClient(connectionString);

//// Obtener la base de datos y la colección (se crean automáticamente si no existen)
//var database = client.GetDatabase("TiendaDB");
//var collection = database.GetCollection<BsonDocument>("Productos");

//// Crear un documento JSON
//var nuevoProducto = new BsonDocument
//{
//    { "nombre", "Teclado Mecánico" },
//    { "precio", 85.50 },
//    { "stock", 50 }
//};

//// Insertar
//await collection.InsertOneAsync(nuevoProducto);
//Console.WriteLine("¡Producto guardado en Cosmos DB!");

//// Buscar
//var filtro = Builders<BsonDocument>.Filter.Eq("nombre", "Teclado Mecánico");
//var producto = await collection.Find(filtro).FirstOrDefaultAsync();
//Console.WriteLine($"Encontrado: {producto}");

// 2. Crear un cliente de MongoDB
MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
var client = new MongoClient(settings);

// 3. Conectar a la base de datos y obtener una colección
var database = client.GetDatabase("PruebaDb");
var collection = database.GetCollection<BsonDocument>("Usuarios");

// 4. Crear un documento e insertarlo
var document = new BsonDocument
        {
            { "Nombre", "Juan" },
            { "Rol", "Desarrollador" },
            { "Activo", true }
        };

collection.InsertOne(document);
Console.WriteLine("¡Documento insertado correctamente en Cosmos DB!");

// 5. Consultar el documento
var filter = Builders<BsonDocument>.Filter.Eq("Nombre", "Juan");
var resultado = collection.Find(filter).FirstOrDefault();

Console.WriteLine($"Usuario encontrado: {resultado["Nombre"]}");
