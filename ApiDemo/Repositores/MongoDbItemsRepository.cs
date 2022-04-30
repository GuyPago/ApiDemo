using ApiDemo.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ApiDemo.Repositores
{
    public class MongoDbItemsRepository : IItemsRepository
    {
        private const string _databaseName = "catalog";
        private const string _collectionName = "items";
        private readonly IMongoCollection<Item> _itemsCollection;

        public MongoDbItemsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(_databaseName);
            _itemsCollection = database.GetCollection<Item>(_collectionName);
        }

        public async Task CreateItemAsync(Item item)
        {
            await _itemsCollection.InsertOneAsync(item);
        }

        public async Task DeleteItemAsync(Guid id)
        {
            await _itemsCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<Item?> GetItemAsync(Guid id)
        {
            Item foundItem = await _itemsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return foundItem;
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return (await _itemsCollection.FindAsync(new BsonDocument())).ToList();
        }

        public async Task UpdateItemAsync(Item item)
        {
            await _itemsCollection.ReplaceOneAsync(existingItem => existingItem.Id == item.Id, item);
        }
    }
}
