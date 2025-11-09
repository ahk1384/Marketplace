using Marketplace.Domain;

namespace Marketplace.Application
{
    public interface IItemService
    {
        Task<bool> AddItemAsync(string name, int price, string description, int ram = 0, int storage = 0);
        Task<bool> RemoveItemAsync(int itemId);
        Task<bool> BuyItemAsync(string username, int itemId);
        Task<IReadOnlyList<Item>> GetAllItemsAsync();
        Task<Item?> GetItemByIdAsync(int itemId);
    }
}