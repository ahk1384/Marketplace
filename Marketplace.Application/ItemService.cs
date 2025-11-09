using Marketplace.Application.Interfaces;
using Marketplace.Domain;

namespace Marketplace.Application
{

    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IUserRepository _userRepository;

        public ItemService(IItemRepository itemRepository, IUserRepository userRepository)
        {
            _itemRepository = itemRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> AddItemAsync(string name, int price, string description, int ram = 0, int storage = 0)
        {
            var item = new Item(name, description, price, ram, storage);
            
            if (!item.IsValidForPurchase())
                return false;

            await _itemRepository.AddAsync(item);
            await _itemRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveItemAsync(int itemId)
        {
            var item = await _itemRepository.GetByIdAsync(itemId);
            if (item != null)
            {
                await _itemRepository.RemoveAsync(item);
                await _itemRepository.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> BuyItemAsync(string username, int itemId)
        {
            var item = await _itemRepository.GetByIdAsync(itemId);
            var user = await _userRepository.GetByUsernameAsync(username);

            if (item != null && user != null && user.CanAfford(item.Price))
            {
                user.DeductBalance(item.Price);
                await _userRepository.UpdateAsync(user);
                await _itemRepository.RemoveAsync(item);
                
                await _userRepository.SaveChangesAsync();
                await _itemRepository.SaveChangesAsync();
                
                return true;
            }
            return false;
        }

        public async Task<IReadOnlyList<Item>> GetAllItemsAsync()
        {
            var items = await _itemRepository.GetAllAsync();
            return items.ToList().AsReadOnly();
        }

        public async Task<Item?> GetItemByIdAsync(int itemId)
        {
            return await _itemRepository.GetByIdAsync(itemId);
        }
    }
}