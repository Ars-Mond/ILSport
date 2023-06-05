using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using ILSport.Framework;
using ILSport.Framework.Core;
using ILSport.Framework.ViewModel;

namespace ILSport.Windows;

public partial class RegisterWindow : Window
{
    public RegisterWindow()
    {
        InitializeComponent();
        DataContext = new RegisterModel(ErrorBox, LoginInput, PasswordInput, NameInput);
    }
}