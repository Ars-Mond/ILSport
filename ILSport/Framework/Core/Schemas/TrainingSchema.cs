using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ILSport.Framework.Core.Schemas;

public sealed class TrainingSchema
{
    public int Id { get; set; }
    public string NameIndex { get; set; } = null!;
    public string? Name { get; set; }

    public int TrainingGroupId { get; set; }
    public TrainingGroupSchema TrainingGroup { get; set; } = null!;
    public ICollection<UserTrainingSchema> UserTrainings { get; set; } = new ObservableCollection<UserTrainingSchema>();

    public TrainingSchema()
    { }
    
    public TrainingSchema(string nameIndex, string name)
    {
        NameIndex = nameIndex;
        Name = name;
    }
    
    public TrainingSchema(int id, string nameIndex, string name)
    {
        Id = id;
        NameIndex = nameIndex;
        Name = name;
    }
}