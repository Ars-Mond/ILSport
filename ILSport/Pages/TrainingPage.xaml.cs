using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using ILSport.Custom;
using ILSport.Framework.Core.Schemas;

namespace ILSport.Pages;

public partial class TrainingPage : Page
{
    public ObservableCollection<TrainingGroupSchema> TrainingGroup { get; private set; }
    public ObservableCollection<TrainingSchema> Training { get; private set; }
    public TrainingPage(ObservableCollection<TrainingGroupSchema> trainingGroup, ObservableCollection<TrainingSchema> training)
    {
        TrainingGroup = trainingGroup;
        Training = training;

        InitializeComponent();
        DataContext = this;
        
        foreach (var group in trainingGroup)
        {
            MainBox.Children.Add(new TrainingBrick(string.IsNullOrEmpty(group.Name) ? "LOL" : group.Name, group.NameIndex));
        }
    }
}