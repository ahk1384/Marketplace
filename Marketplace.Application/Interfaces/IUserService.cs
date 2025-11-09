using Marketplace.Application.Interfaces;
using Marketplace.Domain;

namespace Marketplace.Application
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(string name, string password, int age, string phoneNumber, string email);
        Task<bool> LoginUserAsync(string name, string password);
        Task<bool> EditUserAsync(string name, int age, string phoneNumber, string email);
        Task<User?> GetUserByNameAsync(string name);
        Task<User?> AuthenticateAsync(string name, string password);
        Task<bool> AddBalanceAsync(string name, int amount);
        Task<string> GetAllUsersAsync();
    }
}
