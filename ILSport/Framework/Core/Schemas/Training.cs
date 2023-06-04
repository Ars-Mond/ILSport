using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ILSport.Framework.Core.Schemas;

public class Training
{
    public int Id { get; set; }
    public string NameIndex { get; set; } = null!;
    public string? Name { get; set; }

    public int TrainingGroupId { get; set; }
    public TrainingGroup TrainingGroup { get; set; } = null!;
    public virtual ICollection<UserTraining> UserTrainings { get; set; } = new ObservableCollection<UserTraining>();

    public Training()
    { }
    
    public Training(string nameIndex, string name)
    {
        NameIndex = nameIndex;
        Name = name;
    }
    
    public Training(int id, string nameIndex, string name)
    {
        Id = id;
        NameIndex = nameIndex;
        Name = name;
    }
}