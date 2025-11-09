namespace Marketplace.Domain
{
    public class User
    {
        public int UserId { get; private set; }
        public string Username { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
        public int Age { get; private set; }
        public int Balance { get; private set; }
        public string PhoneNumber { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;

        private User() 
        { 
            Balance = 0; 
        }

        public User(string username, string password, int age, string phoneNumber, string email)
        {
            Username = username;
            Password = password;
            Age = age;
            Balance = 0;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public void AddBalance(int amount)
        {
            if (amount > 0)
                Balance += amount;
        }

        public bool CanAfford(int amount)
        {
            return Balance >= amount;
        }

        public void DeductBalance(int amount)
        {
            if (CanAfford(amount))
                Balance -= amount;
        }

        public void UpdateProfile(int age, string phoneNumber, string email)
        {
            Age = age;
            PhoneNumber = phoneNumber;
            Email = email;
        }
    }
}