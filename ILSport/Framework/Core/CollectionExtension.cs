using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ILSport.Framework.Core.Schemas;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ILSport.Framework.Core;

public static class CollectionExtension
{
    public static List<UserSchema> GetUsers(this Collections collections)
    {
        return collections.DatabaseContext.Users.ToList();
    }

    public static List<TrainingSchema> GetTraining(this Collections collections)
    {
        return collections.DatabaseContext.Trainings.ToList();
    }
    
    public static List<TrainingGroupSchema> GetTrainingGroup(this Collections collections)
    {
        return collections.DatabaseContext.TrainingGroups.ToList();
    }
    
    public static List<UserTrainingSchema> GetUserTraining(this Collections collections)
    {
        return collections.DatabaseContext.UserTrainings.ToList();
    }
    
    
    public static ObservableCollection<UserSchema> GetUsersObservableCollection(this Collections collections)
    {
        return collections.DatabaseContext.Users.Local.ToObservableCollection();
    }

    public static ObservableCollection<TrainingSchema> GetTrainingObservableCollection(this Collections collections)
    {
        return collections.DatabaseContext.Trainings.Local.ToObservableCollection();
    }
    
    public static ObservableCollection<TrainingGroupSchema> GetTrainingGroupObservableCollection(this Collections collections)
    {
        return collections.DatabaseContext.TrainingGroups.Local.ToObservableCollection();
    }
    
    public static EntityEntry<UserSchema>? FindUser(this Collections collections, string login)
    {
        return collections.DatabaseContext.Users.Local.FindEntry(nameof(UserSchema.Login), login);
    }

    public static EntityEntry<TrainingSchema>? FindTraining(this Collections collections, string nameIndex)
    {
        return collections.DatabaseContext.Trainings.Local.FindEntry(nameof(TrainingSchema.NameIndex), nameIndex);
    }
    
    public static EntityEntry<TrainingGroupSchema>? FindTrainingGroup(this Collections collections, string nameIndex)
    {
        return collections.DatabaseContext.TrainingGroups.Local.FindEntry(nameof(TrainingSchema.NameIndex), nameIndex);
    }
}