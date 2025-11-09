using Microsoft.AspNetCore.Mvc;
using Marketplace.Application;

[ApiController]
[Route("item-apis")]
public class ItemsController : ControllerBase
{
    private readonly IItemService _itemService;

    public ItemsController(IItemService itemService)
    {
        _itemService = itemService;
    }

    [HttpGet("get-all-items")]
    public async Task<IActionResult> GetAllItems()
    {
        var items = await _itemService.GetAllItemsAsync();
        return Ok(items);
    }

    [HttpGet("get-item/{id}")]
    public async Task<IActionResult> GetItem(int id)
    {
        var item = await _itemService.GetItemByIdAsync(id);
        if (item == null)
        {
            return NotFound("Item not found");
        }
        return Ok(item);
    }

    [HttpPost("add-item")]
    public async Task<IActionResult> AddItem([FromBody] AddItemRequest request)
    {
        if (await _itemService.AddItemAsync(request.Name, request.Price, request.Description, request.Ram, request.Storage))
        {
            return Ok("Item added successfully");
        }
        return BadRequest("Failed to add item - Invalid data");
    }

    [HttpDelete("remove-item/{id}")]
    public async Task<IActionResult> RemoveItem(int id)
    {
        if (await _itemService.RemoveItemAsync(id))
        {
            return Ok("Item removed successfully");
        }
        return NotFound("Item not found");
    }

    [HttpPost("buy-item")]
    public async Task<IActionResult> BuyItem([FromBody] BuyItemRequest request)
    {
        if (await _itemService.BuyItemAsync(request.Username, request.ItemId))
        {
            return Ok("Item purchased successfully");
        }
        return BadRequest("Purchase failed - Insufficient balance or item not found");
    }
}

public class AddItemRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Price { get; set; }
    public int Ram { get; set; } = 0;
    public int Storage { get; set; } = 0;
}

public class BuyItemRequest
{
    public string Username { get; set; } = string.Empty;
    public int ItemId { get; set; }
}//}