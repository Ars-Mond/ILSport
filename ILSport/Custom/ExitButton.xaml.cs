using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors.Core;

namespace ILSport.Custom;

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
}