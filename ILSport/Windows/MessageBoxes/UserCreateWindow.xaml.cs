using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ILSport.Framework;
using ILSport.Framework.Core.Schemas;

namespace ILSport.Windows.MessageBoxes;

public partial class UserCreateWindow : Window
{
    public ICommand OkCommand { get; }
    public ICommand CancelCommand { get; }
    
    public string Login { get; set; }
    public string Password { get; set; }
    
    public string FamilyName { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    
    public UserType Type { get; set; }
    public UserCreateWindow()
    {
        InitializeComponent();
        DataContext = this;
        ComboBox.ItemsSource = Enum.GetValues(typeof(UserType)).Cast<UserType>();

        OkCommand = new DelegateCommand(Ok);
        CancelCommand = new DelegateCommand(Cancel);
    }

    private void Cancel(object? obj)
    {
        DialogResult = false;
    }

    private void Ok(object? obj)
    {
        SetStateError(false, "Error.");
        if (!CheckString(Login))
        {
            SetStateError(true, "Error login.");
            return;
        }
        if (!CheckString(Password))
        {
            SetStateError(true, "Error password.");
            return;
        }

        DialogResult = true;
    }

    private bool CheckString(string str)
    {
        return str.Split(new[] { ' ' }).Length == 1 && !string.IsNullOrEmpty(str);
    }

    private void SetStateError(bool visible, string text)
    {
        ErrorTextBox.Visibility = visible ? Visibility.Visible : Visibility.Hidden;

        ErrorTextBox.Text = text;
    }
}