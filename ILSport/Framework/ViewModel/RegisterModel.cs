using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ILSport.Custom;
using ILSport.Framework.Core;
using ILSport.Framework.Core.Schemas;

namespace ILSport.Framework.ViewModel
{
    public class RegisterModel : BaseViewModel
    {
        public ICommand SubmitCommand { get; }
        public ICommand TransitionCommand { get; }
    
        public TextBlock ErrorTextBlock { get; }
        public Input LoginField { get; }
        public Input PasswordField { get; }
        public Input NameField { get; }

        public RegisterModel(TextBlock errorTextBlock, Input loginField, Input passwordField, Input nameField)
        {
            ErrorTextBlock = errorTextBlock;
            LoginField = loginField;
            PasswordField = passwordField;
            NameField = nameField;
        
            SubmitCommand = new DelegateCommand(Register);
            TransitionCommand = new DelegateCommand(Transition);
        }

        private void Register(object? obj)
        {
            SetStateErrorTextBlock(false,"Ошибка.");

            var f = Collections.Instance.FindUser(LoginField.Value);
            if (f != null)
            {
                SetStateErrorTextBlock(true,"Пользователь существует.");
                return;
            }

            var g = NameField.Value.Split(new []{' ', '_'});
        
            if (g.Length != 3)
            {
                SetStateErrorTextBlock(true,"Ошибка в ФИО.");
                return;
            }
            
            var user = new UserSchema(LoginField.Value, PasswordField.Value, UserType.USER) { FamilyName = g[0], FirstName = g[1], MiddleName = g[2]};
            Collections.Instance.SetCreatedUser(user);
        
            LoginField.ClearInput();
            NameField.ClearInput();
            PasswordField.ClearInput();
        
            WindowsProvider.Instance.CurrentUser = user;
            WindowsProvider.Instance.AddStateToHistory(WindowType.Login);
            WindowsProvider.Instance.Switch(WindowType.Main);
        }
    
        private void SetStateErrorTextBlock(bool visible, string text = "Error")
        {
            ErrorTextBlock.Visibility = visible ? Visibility.Visible : Visibility.Hidden;
            ErrorTextBlock.Text = text;
        }

        private void Transition(object? obj)
        {
            WindowsProvider.Instance.Switch(WindowType.Login);
        }
    }
}