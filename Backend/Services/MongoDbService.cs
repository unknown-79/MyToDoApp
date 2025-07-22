using Backend.Models;
using MongoDB.Driver;

namespace Backend.Services

{
    public class MongoDbService
    {
        public IMongoDatabase Database { get; }

        public MongoDbService(IConfiguration config)
        {
            
            var connectionString = config["mongodb:connectionString"];
            var databaseName = config["mongodb:Database"];
            var client = new MongoClient(connectionString);
            Database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<TodoItem> GetTodoCollection()
        {
            return Database.GetCollection<TodoItem>("ToDo");
        }
        
    }
}