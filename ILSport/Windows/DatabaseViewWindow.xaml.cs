using System.Windows;
using ILSport.Framework.ViewModel;

namespace ILSport.Windows;

public partial class DatabaseViewWindow : Window
{
    public DatabaseViewWindow()
    {
        InitializeComponent();
        var model =  new DatabaseViewModel();
        DataContext = model;
        ExitButton.CloseAppCommand = model.ReturnCommand;
    }
}