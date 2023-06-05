using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using ILSport.Framework.Core;
using ILSport.Framework.Core.Schemas;

namespace ILSport.Windows;

public partial class TestDataWindow : Window
{
    public List<UserSchema> Users { get; set; }
    public List<TrainingSchema> Training { get; set; }
    public List<TrainingGroupSchema> TrainingGroup { get; set; }
    public List<UserTrainingSchema> UserTrainings { get; set; }

    public TestDataWindow()
    {
        var dd = Collections.Instance.DatabaseContext;
        Users = dd.Users.ToList();
        Training = dd.Trainings.ToList();
        TrainingGroup = dd.TrainingGroups.ToList();
        UserTrainings = dd.UserTrainings.ToList();
        InitializeComponent();
        DataContext = this;
    }
}