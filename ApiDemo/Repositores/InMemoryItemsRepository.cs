using ApiDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemo.Repositores
{
    public class InMemoryItemsRepository : IItemsRepository 
    { 
    
        private List<Item> _items = new List<Item>()
        {
            new Item() { Id = Guid.NewGuid(), Name = "Potion", Price = 8, CreatedDate = DateTimeOffset.UtcNow},
            new Item() { Id = Guid.NewGuid(), Name = "Betanir's Sword", Price = 49, CreatedDate = DateTimeOffset.UtcNow},
            new Item() { Id = Guid.NewGuid(), Name = "Guy's Shield", Price = 36, CreatedDate = DateTimeOffset.UtcNow},
            new Item() { Id = Guid.NewGuid(), Name = "Ram's Belly", Price = 55, CreatedDate = DateTimeOffset.UtcNow},
        };
        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await Task.FromResult(_items);
        }
        public async Task<Item?> GetItemAsync(Guid id)
        {
            Item? item = _items.SingleOrDefault(x => x.Id == id);
            return await Task.FromResult(item);
        }
        public async Task CreateItemAsync(Item item)
        {
            _items.Add(item);
            await Task.CompletedTask;
        }

        public async Task DeleteItemAsync(Guid id)
        {
            int itemIndex = _items.FindIndex(x => x.Id == id);
            _items.RemoveAt(itemIndex);
            await Task.CompletedTask;
        }

        public async Task UpdateItemAsync(Item item)
        {
            int itemIndex = _items.FindIndex(x => x.Id == item.Id);
            _items[itemIndex] = item;
            await Task.CompletedTask;
        }
    }
}
