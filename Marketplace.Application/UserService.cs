using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Marketplace.Domain;
using Marketplace.Infrastructure;

namespace Marketplace.Application
{
    public class UserService : IUserService
    {
        private readonly DatabaseManager _context;

        public UserService(DatabaseManager context)
        {
            _context = context;
        }

        public bool EditUser(string name, int age, string phoneNumber, string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == name);
            if (user != null)
            {
                user.Age = age;
                user.PhoneNumber = phoneNumber;
                user.Email = email;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool LoginUser(string name, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == name && u.Password == password);
            if (user != null)
            {
                Console.WriteLine("Login successful");
                return true;
            }
            return false;
        }

        public bool RegisterUser(string name, string password, int age, string phoneNumber, string email)
        {
            // Check if user already exists
            if (_context.Users.Any(u => u.Username == name))
            {
                return false; // User already exists
            }

            var user = new User(name, password, age, phoneNumber, email);
            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }

        public User? GetUserByName(string name)
        {
            return _context.Users.FirstOrDefault(u => u.Username == name);
        }

        public User? Authenticate(string name, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == name && u.Password == password);
            if (user != null)
            {
                Console.WriteLine("Login successful");
                return user;
            }
            return null;
        }

        public bool AddBalance(string name, int amount)
        {
            var user = GetUserByName(name);
            if (user == null) return false;
            
            user.Balance += amount;
            _context.SaveChanges();
            return true;
        }

        public string GetAllUser()
        {
            string res = string.Empty;  
            foreach(var w in _context.Users)
            {
                res += ("Name : " + w.Username + " | Age : " + w.Age + " | Balance : " + w.Balance + "\n");
            }
            return res;
        }
    }
}
