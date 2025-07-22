using Backend.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Backend.Services
{
    public class TodoService
    {
        private readonly IMongoCollection<TodoItem> _todoCollection;

        public TodoService(MongoDbService dbService)
        {
            _todoCollection = dbService.GetTodoCollection();
        }

        public async Task CreateTodoAsync(TodoItem item)
        {
            if (string.IsNullOrEmpty(item.Id) || !ObjectId.TryParse(item.Id, out _))
            {
                item.Id = null;
            }
            await _todoCollection.InsertOneAsync(item);
        }
    }
};

