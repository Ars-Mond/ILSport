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
        _databaseContext.TrainingGroups.Load();
        _databaseContext.Trainings.Load();
        _databaseContext.UserTrainings.Load();

        var user = _databaseContext.Users.ToList().Find((i) => i.Login == "root");
        if (user == null)
        {
            var u = new User("root", "root", UserType.ADMIN);
            _databaseContext.Users.Add(u);
            _databaseContext.SaveChanges();
        }
        Debug.WriteLine(_databaseContext.Users.Count());

        if (CreateTrainingGroup("hands", "Руки"))
        {
            Debug.WriteLine("Create hands group");
        }
        if (CreateTrainingGroup("legs", "Ноги"))
        {
            Debug.WriteLine("Create legs group");
        }
        
        Debug.WriteLine(_databaseContext.TrainingGroups.Count());

        if (CreateTraining("push-ups", "Отжимания", "hands"))
        {
            Debug.WriteLine("Create push-ups training");
        }
        if (CreateTraining("push-ups", "Отжимания", "legs"))
        {
            Debug.WriteLine("Create push-ups training2");
        }
        if (CreateTraining("squats", "Приседания", "legs"))
        {
            Debug.WriteLine("Create squats training");
        }
        
        Debug.WriteLine(_databaseContext.Trainings.Count());
    }

    public EntityEntry<User>? FindUser(string login)
    {
        return _databaseContext.Users.Local.FindEntry(nameof(User.Login), login);
    }

    public EntityEntry<Training>? FindTraining(string nameIndex)
    {
        return _databaseContext.Trainings.Local.FindEntry(nameof(Training.NameIndex), nameIndex);
    }

    // TODO метод расширения
    public bool CreateTraining(string trainingIndex, string trainingName, string trainingGroup)
    {
        var tg = _databaseContext.TrainingGroups.Local.FindEntry(nameof(TrainingGroup.NameIndex), trainingGroup);
        if (tg == null) return false;

        var training = FindTraining(trainingIndex);
        if (training != null) return false;
        
        var t = new Training(trainingIndex, trainingName) { TrainingGroup = tg.Entity };
        _databaseContext.Trainings.Add(t);
        _databaseContext.SaveChanges();
        return true;
    }
    
    public bool CreateTrainingGroup(string trainingGroupIndex, string trainingGroupName)
    {

        var trainingGroup = _databaseContext.TrainingGroups.Local.FindEntry(nameof(TrainingGroup.NameIndex), trainingGroupIndex);
        if (trainingGroup != null) return false;

        var tg = new TrainingGroup(trainingGroupIndex, trainingGroupName);
        _databaseContext.TrainingGroups.Add(tg);
        _databaseContext.SaveChanges();
        return true;
    }
    
    public bool TryFindUser(string login, out EntityEntry<User>? result)
    {
       var i = _databaseContext.Users.Local.FindEntry(nameof(User.Login), login);
       result = i;
       return i != null;
    }
}

//Debug.WriteLine(_databaseContext.Users.Local.FindEntry(nameof(User.Login), "root"));

/*var training = _databaseContext.Trainings.Local.FindEntry(nameof(Training.NameIndex), "push-ups");
if (training == null)
{
    var t = new Training("push-ups", "Отжимания");
    //t.TrainingGroup = _databaseContext.TrainingGroups.Local.FindEntry(nameof(TrainingGroup.NameIndex), "hands")!.Entity;
    _databaseContext.Trainings.Add(t);
    _databaseContext.SaveChanges();
}*/
        
/*var trainingGroup = _databaseContext.TrainingGroups.Local.FindEntry(nameof(TrainingGroup.NameIndex), "hands");
if (trainingGroup == null)
{
   var tG = new TrainingGroup("hands", "Руки");
   _databaseContext.TrainingGroups.Add(tG);
   _databaseContext.SaveChanges();
}*/