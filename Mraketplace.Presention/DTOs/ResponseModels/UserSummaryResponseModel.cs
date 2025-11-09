namespace Mraketplace.Presention.DTOs.ResponseModels
{
    public class UserSummaryResponseModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int Balance { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public UserSummaryResponseModel(string name, int age)
        {
            Name = name;
            Age = age;
            Balance = 0;
            Email = string.Empty;
            PhoneNumber = string.Empty;
        }

        public UserSummaryResponseModel(string name, int age, int balance, string email, string phoneNumber)
        {
            Name = name;
            Age = age;
            Balance = balance;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}