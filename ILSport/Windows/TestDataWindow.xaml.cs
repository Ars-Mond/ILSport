using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using ILSport.Framework.Core;
using ILSport.Framework.Core.Schemas;

namespace ILSport.Windows;

public partial class TestDataWindow : Window
{
    public List<User> Users { get; set; }
    public List<Training> Training { get; set; }
    public List<TrainingGroup> TrainingGroup { get; set; }
    public List<UserTraining> UserTrainings { get; set; }

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