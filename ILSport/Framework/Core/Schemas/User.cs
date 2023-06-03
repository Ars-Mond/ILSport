namespace ILSport.Framework.Core.Schemas;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public UserType Type { get; set; }
    
    public User() {}

    public User(int id, string login, string password, UserType type)
    {
        Id = id;
        Login = login;
        Password = password;
        Type = type;
    }
    public User(string login, string password, UserType type)
    {
        Login = login;
        Password = password;
        Type = type;
    }
}

public enum UserType
{
    USER,
    ADMIN
} 