using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using ILSport.Framework.Core.Schemas;

namespace ILSport.Pages;

public partial class ItemTrainingPage : Page
{
    public ItemTrainingPage(List<TrainingSchema> training)
    {
        InitializeComponent();
        DataContext = this;
    }
}