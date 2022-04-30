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

        public void CreateItem(Item item)
        {
            _itemsCollection.InsertOne(item);
        }

        public void DeleteItem(Guid id)
        {
            _itemsCollection.DeleteOne(x => x.Id == id);
        }

        public Item? GetItem(Guid id)
        {
            Item foundItem = _itemsCollection.Find(x => x.Id == id).FirstOrDefault();
            return foundItem;
        }

        public IEnumerable<Item> GetItems()
        {
            return _itemsCollection.Find(new BsonDocument()).ToList();
        }

        public void UpdateItem(Item item)
        {
            _itemsCollection.ReplaceOne(existingItem => existingItem.Id == item.Id, item);
        }
    }
}
