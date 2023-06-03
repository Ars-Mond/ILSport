using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using ILSport.Framework;
using ILSport.Framework.Core;

namespace ILSport.Windows;

public partial class RegisterWindow : Window
{
    public ICommand LoginCommand { get; }
    public ICommand SubmitCommand { get; }
    public RegisterWindow()
    {
        InitializeComponent();
        DataContext = this;

        LoginCommand = new DelegateCommand(OnLogin);
        SubmitCommand = new DelegateCommand(OnSubmit);
    }
    
    private void OnSubmit(object? obj)
    {
        Debug.WriteLine(LoginInput.Value);
        Debug.WriteLine(PasswordInput.Value);
    }

    private void OnLogin(object? obj)
    {
        WindowsProvider.Instance.Switch(WindowType.Login);
    }
}