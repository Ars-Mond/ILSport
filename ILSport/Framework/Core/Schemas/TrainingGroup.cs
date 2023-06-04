using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ILSport.Framework.Core.Schemas;

public class TrainingGroup
{
    public int Id { get; set; }
    public string NameIndex { get; set; } = null!;
    public string? Name { get; set; }

    public List<Training> Trainings { get; private set; } = null!;


    public TrainingGroup()
    { }
    
    public TrainingGroup(string nameIndex, string name)
    {
        NameIndex = nameIndex;
        Name = name;
    }

    public TrainingGroup(int id, string nameIndex, string name)
    {
        Id = id;
        NameIndex = nameIndex;
        Name = name;
    }
}