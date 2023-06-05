using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ILSport.Custom;
using ILSport.Framework.Core;
using ILSport.Framework.Core.Schemas;
using ILSport.Pages;

namespace ILSport.Framework.ViewModel.Controllers;

public class TrainingPageController : IPageController
{
    private readonly TrainingPage _trainingPage;
    private List<TrainingBrick> _trainingBricks;
    private ICommand _brickCommand;
    private Page? _generatePage;

    public MenuItemType MenuItemType { get; } = MenuItemType.Training;
    
    
    public event Action<Page>? OnSetPage;

    
    public Page GetPage() // page
    {
        return _generatePage ?? _trainingPage;
    }

    public TrainingPageController(ObservableCollection<TrainingGroupSchema> trainingGroup, ObservableCollection<TrainingSchema> training)
    {
        _brickCommand = new DelegateCommand(o => OpenBrickPage((string)o!));
        _trainingPage = new TrainingPage(trainingGroup, training);

        _trainingBricks = (from UIElement child in _trainingPage.MainBox.Children select (child as TrainingBrick)!).ToList();
        
    }

    private void OpenBrickPage(string s)
    {
        var f = Collections.Instance.FindTrainingGroup(s);
        if (f == null) return;
        
        _generatePage = new ItemTrainingPage(f.Entity.Trainings);
        OnSetPage?.Invoke(_generatePage);
    }
}