using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors.Core;

namespace ILSport.Custom
{
    public partial class ExitButton : UserControl, INotifyPropertyChanged
    {
        public ICommand CloseAppCommand { get; set; }

        public void CloseApp()
        {
            Application.Current.Shutdown();
        }

        public ExitButton()
        {
            InitializeComponent();
            DataContext = this;

            CloseAppCommand = new ActionCommand(CloseApp);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}