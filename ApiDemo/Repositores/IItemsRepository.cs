using ApiDemo.Models;

namespace ApiDemo.Repositores
{
    public interface IItemsRepository
    {
        public IEnumerable<Item> GetItems();
        public Item GetItem(Guid id);
        public void CreateItem(Item item);
        public void UpdateItem(Item item);
        public void DeleteItem(Guid id);
    }
}