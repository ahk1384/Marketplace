using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.Domain
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        
        [Required]
        public string Username { get; set; } = string.Empty;
        
        [Required]
        public string Password { get; set; } = string.Empty;
        
        [Required]
        public int Age { get; set; }
        
        public int Balance { get; set; }
        
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        
        public string Email { get; set; } = string.Empty;

        // Parameterless constructor for Entity Framework
        public User()
        {
            Balance = 0;
        }

        public User(string name, string password, int age, string phoneNumber, string email)
        {
            this.Username = name;
            this.Password = password;
            this.Age = age;
            this.Balance = 0;
            this.PhoneNumber = phoneNumber;
            this.Email = email;
        }
    }
}