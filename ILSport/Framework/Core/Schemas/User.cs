using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ILSport.Framework.Core.Schemas;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    public UserType Type { get; set; }
    
    //public UserStats UserStats { get; set; }
    
    public string? FirstName { get; set; }
    public string? FamilyName { get; set; }
    public string? MiddleName { get; set; }
    public string? Birthday { get; set; }
    public double? Weight { get; set; }
    public double? Sleep { get; set; }

    public virtual ICollection<UserTraining> UserTrainings { get; set; } = new ObservableCollection<UserTraining>();

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