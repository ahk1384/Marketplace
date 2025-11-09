using Marketplace.Domain;

namespace Marketplace.Application.Interfaces
{
    public interface IItemRepository
    {
        Task<Item?> GetByIdAsync(int id);
        Task<IEnumerable<Item>> GetAllAsync();
        Task AddAsync(Item item);
        Task RemoveAsync(Item item);
        Task SaveChangesAsync();
    }
}