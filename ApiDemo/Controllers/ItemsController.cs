using ApiDemo.Dtos;
using ApiDemo.Models;
using ApiDemo.Repositores;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository? _repository;

        public ItemsController(IItemsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet] // Get /Item
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            return (await _repository.GetItemsAsync()).Select(x => x.AsDto());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
        {
            Item? item = await _repository.GetItemAsync(id);
            return item == null ? NotFound() : item.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
        {
            Item newItem = new Item
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await _repository.CreateItemAsync(newItem);
            return CreatedAtAction(nameof(GetItemAsync), new { Id = newItem.Id }, newItem.AsDto());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(Guid id)
        {
            Item? itemToRemove = await _repository.GetItemAsync(id);
            if (itemToRemove == null) return NotFound($"Couldn't find item with id of {id}");
            await _repository.DeleteItemAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItem(Guid id, CreateItemDto itemDto)
        {
            Item? existingItem = await _repository.GetItemAsync(id);
            if (existingItem == null) return NotFound($"Couldn't find item with id of {id}");
            Item UpdatedItem = existingItem with
            {
                Name = itemDto.Name,
                Price = itemDto.Price
            };
            await _repository.UpdateItemAsync(UpdatedItem);
            return NoContent();
        }
    }
}
