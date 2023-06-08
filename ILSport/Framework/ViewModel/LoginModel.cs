using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ILSport.Custom;
using ILSport.Framework.Core;
using ILSport.Framework.Core.Schemas;

namespace ILSport.Framework.ViewModel
{
    public class LoginModel : BaseViewModel
    {
    
        public ICommand SubmitCommand { get; }
        public ICommand TransitionCommand { get; }
    
        public TextBlock ErrorTextBlock { get; }
        public Input LoginField { get; }
        public Input PasswordField { get; }
    
    
        public LoginModel(TextBlock errorTextBlock, Input loginField, Input passwordField)
        {
            ErrorTextBlock = errorTextBlock;
            LoginField = loginField;
            PasswordField = passwordField;
        
            SubmitCommand = new DelegateCommand(Login);
            TransitionCommand = new DelegateCommand(Transition);
        }

        private void Login(object? obj)
        {
            SetStateErrorTextBlock(false,"Ошибка.");
        
            var user = Collections.Instance.FindUser(LoginField.Value);

            if (user == null) SetStateErrorTextBlock(true, "Неверный логин или пароль!");
            else
            {
                if (user.Password == PasswordField.Value)
                {
                    if (user.Type == UserType.ADMIN)
                    {
                        var messageBoxResult = MessageBox.Show(
                            "У вас права администратора. Хотите открыть панель администратора?",
                            "Панель Администратора",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Question);
                        if (messageBoxResult == MessageBoxResult.Yes)
                        {
                            WindowsProvider.Instance.AddStateToHistory(WindowType.Login);
                            WindowsProvider.Instance.Switch(WindowType.DatabaseView);
                            return;
                        }
                    }

                    LoginField.ClearInput();
                    PasswordField.ClearInput();

                    WindowsProvider.Instance.CurrentUser = user;
                    WindowsProvider.Instance.AddStateToHistory(WindowType.Login);
                    WindowsProvider.Instance.Switch(WindowType.Main);
                }
                else
                {
                    SetStateErrorTextBlock(true, "Неверный логин или пароль!");
                }
            }
        }

        private void SetStateErrorTextBlock(bool visible, string text = "Error")
        {
            ErrorTextBlock.Visibility = visible ? Visibility.Visible : Visibility.Hidden;
            ErrorTextBlock.Text = text;
        }
    
        private void Transition(object? obj)
        {
            WindowsProvider.Instance.Switch(WindowType.Register);
        }
    }
}