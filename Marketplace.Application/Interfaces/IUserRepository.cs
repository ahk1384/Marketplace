using Marketplace.Domain;

namespace Marketplace.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByIdAsync(int id);
        Task<bool> ExistsAsync(string username);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task SaveChangesAsync();
        Task<IEnumerable<User>> GetAllAsync();
    }
}