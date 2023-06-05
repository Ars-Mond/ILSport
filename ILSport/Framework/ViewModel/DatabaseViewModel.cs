using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using ILSport.Framework.Core;
using ILSport.Framework.Core.Schemas;
using ILSport.Windows.MessageBoxes;

namespace ILSport.Framework.ViewModel;

public class DatabaseViewModel : BaseViewModel
{

    public ICommand ReturnCommand { get;}
    public ICommand CreateUserCommand { get; }
    public ICommand UpdateUserCommand { get; }
    public ICommand DeleteUserCommand { get; }
    
    public ICommand CreateTrainingCommand { get; }
    public ObservableCollection<UserSchema> Users { get; set; }
    public UserSchema? SelectedUser { get; set; }
    
    public ObservableCollection<TrainingSchema> Training { get; set; }
    
    public ObservableCollection<TrainingGroupSchema> TrainingGroup { get; set; }
    
    public ObservableCollection<UserTrainingSchema> UserTrainings { get; set; }
    
    
    public UserCreateWindow? UserWindow { get; private set; }
    


    public DatabaseViewModel()
    {
        Users = Collections.Instance.GetUsersObservableCollection();
        TrainingGroup = Collections.Instance.GetTrainingGroupObservableCollection();
        Training = Collections.Instance.GetTrainingObservableCollection();
        UserTrainings = Collections.Instance.GetUserTrainingObservableCollection();

        ReturnCommand = new DelegateCommand(Return);
        
        CreateUserCommand = new DelegateCommand(CreateUser);
        UpdateUserCommand = new DelegateCommand(UpdateUser);
        DeleteUserCommand = new DelegateCommand(DeleteUser);
    }

    private void Return(object? obj)
    {
        if (WindowsProvider.Instance.BackStateFromHistory(true) == false)
            WindowsProvider.Instance.ExitApplication();
    }

    private void CreateUser(object? obj)
    {
        UserWindow = new UserCreateWindow();
        
        var showDialog = UserWindow.ShowDialog();
        if (showDialog is not true) return;
        var user = new UserSchema(UserWindow.Login, UserWindow.Password, UserWindow.Type)
        {
            FamilyName = UserWindow.FamilyName,
            FirstName = UserWindow.FirstName,
            MiddleName = UserWindow.MiddleName
        };
            
        Collections.Instance.SetCreatedUser(user);
    }

    private void UpdateUser(object? obj)
    {
        Collections.Instance.DatabaseContext.SaveChanges();
    }

    private void DeleteUser(object? obj)
    {
        if (SelectedUser == null)
        {
            MessageBox.Show("Объект не выделен", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }
        
        var f = MessageBox.Show("Уверенны что хотите удалить?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (f == MessageBoxResult.Yes)
        {
            Collections.Instance.DatabaseContext.Users.Remove(SelectedUser);
        }
    }
}