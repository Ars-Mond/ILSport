using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ILSport.Framework;
using ILSport.Framework.Core;
using ILSport.Framework.Core.Schemas;

namespace ILSport.Windows;

public partial class LoginWindow : Window
{
    public ICommand RegisterCommand { get; }
    public ICommand SubmitCommand { get; }
    
    public LoginWindow()
    {
        InitializeComponent();
        DataContext = this;

        RegisterCommand = new DelegateCommand(OnRegister);
        SubmitCommand = new DelegateCommand(OnSubmit);
    }

    private void OnSubmit(object? obj)
    {
        ErrorBox.Visibility = Visibility.Hidden;
        ErrorBox.Text = "Ошибка.";
        
        var user = Collections.Instance.FindUser(LoginInput.Value);
        
        if (user != null)
        {
            if (user.Entity.Password == PasswordInput.Value)
            {
                if (user.Entity.Type == UserType.ADMIN)
                {
                    var messageBoxResult = MessageBox.Show("У вас права администратора. Хотите открыть панель администратора?", 
                        "Панель Администратора",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        WindowsProvider.Instance.AddStateToHistory(WindowType.Login);
                        var t2 = WindowsProvider.Instance.Switch(WindowType.TestData);
                        Debug.WriteLine(t2);
                        return;
                    }
                }

                LoginInput.ClearInput();
                PasswordInput.ClearInput();

                WindowsProvider.Instance.AddStateToHistory(WindowType.Login);
                var t = WindowsProvider.Instance.Switch(WindowType.Main, user.Entity);
                Debug.WriteLine(t);
            }
            else
            {
                ErrorBox.Visibility = Visibility.Visible;
                ErrorBox.Text = "Неверный логин или пароль!";
            }
        }
        else
        {
            ErrorBox.Visibility = Visibility.Visible;
            ErrorBox.Text = "Неверный логин или пароль!";
        }
    }

    private void OnRegister(object? obj)
    {
        WindowsProvider.Instance.Switch(WindowType.Register);
    }
}