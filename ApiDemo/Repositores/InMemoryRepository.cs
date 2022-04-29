using ApiDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemo.Repositores
{
    public class InMemoryRepository : IItemsRepository 
    { 
    
        private List<Item> _items = new List<Item>()
        {
            new Item() { Id = Guid.NewGuid(), Name = "Potion", Price = 8, CreatedDate = DateTimeOffset.UtcNow},
            new Item() { Id = Guid.NewGuid(), Name = "Betanir's Sword", Price = 49, CreatedDate = DateTimeOffset.UtcNow},
            new Item() { Id = Guid.NewGuid(), Name = "Guy's Shield", Price = 36, CreatedDate = DateTimeOffset.UtcNow},
            new Item() { Id = Guid.NewGuid(), Name = "Ram's Belly", Price = 55, CreatedDate = DateTimeOffset.UtcNow},
        };
        public IEnumerable<Item> GetItems()
        {
            return _items;
        }
        public Item? GetItem(Guid id)
        {
            Item? item = _items.SingleOrDefault(x => x.Id == id);
            return item;
        }
        public void CreateItem(Item item)
        {
            throw new NotImplementedException();
        }

        public void DeleteItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
