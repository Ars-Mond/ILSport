using System.Windows;
using ILSport.Framework.ViewModel;

namespace ILSport.Windows;

public partial class DatabaseViewWindow : Window
{
    public DatabaseViewWindow()
    {
        InitializeComponent();
        var model = new DatabaseViewModel();
        DataContext = model;
        ExitButton.CloseAppCommand = model.ReturnCommand;
        
        /*UserDataGrid.Columns[10].Visibility = Visibility.Collapsed;
        TrainingGroupDataGrid.Columns[3].Visibility = Visibility.Collapsed;
        TrainingDataGrid.Columns[4].Visibility = Visibility.Collapsed;
        TrainingDataGrid.Columns[5].Visibility = Visibility.Collapsed;*/
    }
}