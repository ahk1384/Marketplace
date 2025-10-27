using Marketplace.Domain;

namespace Marketplace.Application
{
    public interface IUserService
    {
        public bool RegisterUser(string name, string password, int age, string phoneNumber, string email);
        public bool LoginUser(string name, string password);
        public bool EditUser(string name, int age, string phoneNumber, string email);
        public User? GetUserByName(string name);
        public User? Authenticate(string name, string password);
        public bool AddBalance(string name, int amount);
        public string GetAllUser();
    }
}
