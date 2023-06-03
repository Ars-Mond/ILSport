using System.Diagnostics;
using System.Linq;
using ILSport.Framework.Core.Schemas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ILSport.Framework.Core;

public class Collections
{
    private static Collections? _instance;
    public static Collections Instance => _instance ??= new Collections();


    private readonly DatabaseContext _databaseContext;

    public DatabaseContext DatabaseContext => _databaseContext;

    private Collections()
    {
        _databaseContext = new DatabaseContext();

        _databaseContext.Database.EnsureCreated();
        
        _databaseContext.Users.Load();
        
        Debug.WriteLine(_databaseContext.Users.Local.FindEntry(nameof(User.Login), "root"));

        var user = _databaseContext.Users.ToList().Find((i) => i.Login == "root");
        if (user == null)
        {
            var u = new User("root", "root", UserType.ADMIN);
            _databaseContext.Users.Add(u);
            _databaseContext.SaveChanges();
        }
        
        Debug.WriteLine(_databaseContext.Users.Count());
    }

    public EntityEntry<User>? FindUser(string login)
    {
        return _databaseContext.Users.Local.FindEntry(nameof(User.Login), login);
    }
    
    public bool TryFindUser(string login, out EntityEntry<User>? result)
    {
       var i = _databaseContext.Users.Local.FindEntry(nameof(User.Login), login);
       result = i;
       return i != null;
    }
}