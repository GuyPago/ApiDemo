using ApiDemo.Dtos;
using ApiDemo.Models;
using ApiDemo.Repositores;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository? _repository;

        public ItemsController(IItemsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet] // Get /Item
        public IEnumerable<ItemDto> GetItems()
        {
            return _repository.GetItems().Select(x => x.AsDto());
        }

        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            Item? item = _repository.GetItem(id);
            return item == null ? NotFound() : item.AsDto();
        }

        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)
        {
            Item newItem = new Item
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            _repository.CreateItem(newItem);
            return CreatedAtAction(nameof(GetItem), new {Id = newItem.Id}, newItem);
        }
    }
}
