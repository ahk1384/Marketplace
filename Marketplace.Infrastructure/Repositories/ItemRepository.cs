using Marketplace.Application.Interfaces;
using Marketplace.Domain;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Infrastructure.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly DatabaseManager _context;

        public ItemRepository(DatabaseManager context)
        {
            _context = context;
        }

        public async Task<Item?> GetByIdAsync(int id)
        {
            return await _context.Items
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            return await _context.Items
                .OrderByDescending(i => i.CreatedAt)
                .ToListAsync();
        }

        public async Task AddAsync(Item item)
        {
            await _context.Items.AddAsync(item);
        }

        public Task RemoveAsync(Item item)
        {
            _context.Items.Remove(item);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}