using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ILSport.Framework.Core.Schemas;

public sealed class UserSchema
{
    public int Id { get; set; }
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    public UserType Type { get; set; }
    
    public string? Avatar { get; set; }
    public string? FirstName { get; set; }
    public string? FamilyName { get; set; }
    public string? MiddleName { get; set; }
    public string? Birthday { get; set; }
    public double? Weight { get; set; }
    public double? Sleep { get; set; }

    public ICollection<UserTrainingSchema> UserTrainings { get; set; } = new ObservableCollection<UserTrainingSchema>();

    public UserSchema() {}

    public UserSchema(int id, string login, string password, UserType type)
    {
        Id = id;
        Login = login;
        Password = password;
        Type = type;
    }
    public UserSchema(string login, string password, UserType type)
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