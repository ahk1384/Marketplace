using Marketplace.Application.Interfaces;
using Marketplace.Domain;

namespace Marketplace.Application
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> EditUserAsync(string name, int age, string phoneNumber, string email)
        {
            var user = await _userRepository.GetByUsernameAsync(name);
            if (user != null)
            {
                user.UpdateProfile(age, phoneNumber, email);
                await _userRepository.UpdateAsync(user);
                await _userRepository.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> LoginUserAsync(string name, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(name);
            if (user != null && user.Password == password)
            {
                Console.WriteLine("Login successful");
                return true;
            }
            return false;
        }

        public async Task<bool> RegisterUserAsync(string name, string password, int age, string phoneNumber, string email)
        {
            if (await _userRepository.ExistsAsync(name))
            {
                return false;
            }

            var user = new User(name, password, age, phoneNumber, email);
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
            return true;
        }

        public async Task<User?> GetUserByNameAsync(string name)
        {
            return await _userRepository.GetByUsernameAsync(name);
        }

        public async Task<User?> AuthenticateAsync(string name, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(name);
            if (user != null && user.Password == password)
            {
                Console.WriteLine("Login successful");
                return user;
            }
            return null;
        }

        public async Task<bool> AddBalanceAsync(string name, int amount)
        {
            var user = await GetUserByNameAsync(name);
            if (user == null) return false;
            
            user.AddBalance(amount);
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();
            return true;
        }

        public async Task<string> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            var result = string.Empty;
            
            foreach (var user in users)
            {
                result += $"Name: {user.Username} | Age: {user.Age} | Balance: {user.Balance}\n";
            }
            
            return result;
        }
    }
}
