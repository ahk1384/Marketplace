namespace Mraketplace.Presention.DTOs.RequestModels
{
    public class RegisterRequestModel
    {
        public string username { get; }
        public string password { get; }
        public int age { get; }
        public string phoneNumber { get; }
        public string email { get; }
        public RegisterRequestModel(string username, string password, int age, string phoneNumber, string email)
        {
            this.username = username;
            this.password = password;
            this.age = age;
            this.phoneNumber = phoneNumber;
            this.email = email;
        }
    }
}
