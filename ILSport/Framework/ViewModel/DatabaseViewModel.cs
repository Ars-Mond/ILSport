using System;
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
    public ICommand CreateImageCommand { get; }
    public ICommand UpdateCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand UpdatePageCommand { get; }
    public ObservableCollection<UserSchema> Users { get; set; }
    public UserSchema? SelectedUser { get; set; }
    public object? SelectedItem { get; set; }
    
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
        CreateImageCommand = new DelegateCommand(CreateImage);
        UpdateCommand = new DelegateCommand(UpdateUser);
        DeleteCommand = new DelegateCommand(DeleteUser);
        UpdatePageCommand = new DelegateCommand(UpdatePage);
    }

    private void UpdatePage(object? obj)
    {
        Users = Collections.Instance.GetUsersObservableCollection();
        TrainingGroup = Collections.Instance.GetTrainingGroupObservableCollection();
        Training = Collections.Instance.GetTrainingObservableCollection();
        UserTrainings = Collections.Instance.GetUserTrainingObservableCollection();
    }

    private void Return(object? obj)
    {
        if (WindowsProvider.Instance.BackStateFromHistory(true) == false)
            WindowsProvider.Instance.ExitApplication();
    }

    private void CreateUser(object? obj)
    {
        try
        {
            var t = MessageBox.Show("Создание пользователя данным образом устарело. Может вызвать исключение. Уверенны что хотите начать создание?", "Создание", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (t != MessageBoxResult.Yes) return;

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
        catch (Exception e)
        {
            MessageBox.Show("При создании произошла неизвестная ошибка.", "Создание", MessageBoxButton.OK, MessageBoxImage.Error);
            Console.WriteLine(e);
        }
    }

    private void CreateImage(object? obj)
    {
        throw new NotImplementedException();
    }

    private void UpdateUser(object? obj)
    {
        try
        {
            if (obj == null) return;
        
            var f = MessageBox.Show("Уверенны что хотите обновить?", "Обновление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (f == MessageBoxResult.Yes)
            {
                Collections.Instance.DatabaseContext.SaveChanges();
            }
        }
        catch (Exception e)
        {
            MessageBox.Show("При обновлении произошла ошибка. Проверьте нет ли дубликатов в уникальных полях. Пример: <Id>, <Login>.", "Обновление", MessageBoxButton.OK, MessageBoxImage.Error);
            Console.WriteLine(e);
        }
        
    }

    private void DeleteUser(object? obj)
    {
        try
        {
            if (obj == null)
            {
                MessageBox.Show("Объект не выделен", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
        
            var f = MessageBox.Show("Уверенны что хотите удалить?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (f != MessageBoxResult.Yes) return;
            
            Collections.Instance.DatabaseContext.Remove(obj);
            Collections.Instance.DatabaseContext.SaveChanges();
        }
        catch (Exception e)
        {
            MessageBox.Show("При удалении произошла неизвестная ошибка.", "Удаление", MessageBoxButton.OK, MessageBoxImage.Error);
            Console.WriteLine(e);
        }
    }
}