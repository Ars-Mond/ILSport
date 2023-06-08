using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace ILSport.Custom
{
    public partial class Input : UserControl
    {
        public string Title { get; set; }
        public string Value { get; set; } = string.Empty;

        public bool IsPassword
        {
            get => _isPassword;
            set
            {
                _isPassword = value;
                if (IsPassword)
                {
                    StringInputBox.Visibility = Visibility.Collapsed;
                    PasswordInputBox.Visibility = Visibility.Visible;
                }
                else
                {
                    StringInputBox.Visibility = Visibility.Visible;
                    PasswordInputBox.Visibility = Visibility.Collapsed;
                }
            }
        }

        private bool _isPassword = false;

        public Input()
        {
            InitializeComponent();
            DataContext = this;

            if (string.IsNullOrEmpty(Title)) Title = "Title";
            if (string.IsNullOrEmpty(Value)) Value = "";

            StringInputBox.TextChanged += (sender, args) => { Value = StringInputBox.Text; };
            PasswordInputBox.PasswordChanged += (sender, args) => { Value = PasswordInputBox.Password; };
        }

        public void ClearInput()
        {
            StringInputBox.Text = string.Empty;
            PasswordInputBox.Password = string.Empty;
            Value = string.Empty;
        }
    }
}