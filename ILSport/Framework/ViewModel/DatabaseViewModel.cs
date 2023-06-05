using System.Collections.Generic;
using System.Windows.Input;
using ILSport.Framework.Core;
using ILSport.Framework.Core.Schemas;

namespace ILSport.Framework.ViewModel;

public class DatabaseViewModel : BaseViewModel
{

    public ICommand ReturnCommand;
    public List<UserSchema> Users { get; set; }
    public List<TrainingSchema> Training { get; set; }
    public List<TrainingGroupSchema> TrainingGroup { get; set; }
    public List<UserTrainingSchema> UserTrainings { get; set; }


    public DatabaseViewModel()
    {
        Users = Collections.Instance.GetUsers();
        TrainingGroup = Collections.Instance.GetTrainingGroup();
        Training = Collections.Instance.GetTraining();
        UserTrainings = Collections.Instance.GetUserTraining();

        ReturnCommand = new DelegateCommand(Return);
    }

    private void Return(object? obj)
    {
        if (WindowsProvider.Instance.BackStateFromHistory(true) == false)
            WindowsProvider.Instance.ExitApplication();
    }
}