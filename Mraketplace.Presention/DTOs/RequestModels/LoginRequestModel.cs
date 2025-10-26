namespace Mraketplace.Presention.DTOs.RequestModels;

public class LoginRequestModel
{
    public string username { get; }
    public string password { get; }
    
    public LoginRequestModel(string username, string password)
    {
        this.username = username;
        this.password = password;
    }
}