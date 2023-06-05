using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ILSport.Framework.Core.Schemas;

public sealed class TrainingGroupSchema
{
    public int Id { get; set; }
    public string NameIndex { get; set; } = null!;
    public string? Name { get; set; }

    public List<TrainingSchema> Trainings { get; private set; } = null!;


    public TrainingGroupSchema()
    { }
    
    public TrainingGroupSchema(string nameIndex, string name)
    {
        NameIndex = nameIndex;
        Name = name;
    }

    public TrainingGroupSchema(int id, string nameIndex, string name)
    {
        Id = id;
        NameIndex = nameIndex;
        Name = name;
    }
}