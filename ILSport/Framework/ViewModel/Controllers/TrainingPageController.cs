using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ILSport.Custom;
using ILSport.Framework.Core;
using ILSport.Framework.Core.Schemas;
using ILSport.Pages;

namespace ILSport.Framework.ViewModel.Controllers;

public class TrainingPageController : BaseViewModel, IPageController
{
    private readonly TrainingPage _trainingPage;
    private List<TrainingBrick> _trainingBricks = new List<TrainingBrick>();
    private Page? _generatePage;
    private Stack<Page> _historyPage = new Stack<Page>();

    public ICommand ListBrickCommand { get; private set; }
    public ICommand BrickCommand { get; private set; }
    public ICommand BackCommand { get; private set; }
    public ObservableCollection<TrainingGroupSchema> TrainingGroup { get; private set; }
    public ObservableCollection<TrainingSchema> Training { get; private set; }
    
    
    public MenuItemType MenuItemType { get; } = MenuItemType.Training;
    public event Action<Page>? OnSetPage;
    public Page GetPage()
    {
        return _generatePage ?? _trainingPage;
    }

    public TrainingPageController(ObservableCollection<TrainingGroupSchema> trainingGroup, ObservableCollection<TrainingSchema> training)
    {
        TrainingGroup = trainingGroup;
        Training = training;
        
        _trainingPage = new TrainingPage() { DataContext = this };
        
        BrickCommand = new DelegateCommand(o => OpenBrickPage((string)o!));
        ListBrickCommand = new DelegateCommand(o => OpenListBrickPage((string)o!));
        BackCommand = new DelegateCommand(Back);
        
        foreach (var group in TrainingGroup)
        {
            var tb = new TrainingBrick(string.IsNullOrEmpty(group.Name) ? "LOL" : group.Name, group.NameIndex);
            _trainingPage.MainBox.Children.Add(tb);
            tb.Command = BrickCommand;
            _trainingBricks.Add(tb);
        }
    }

    private void OpenBrickPage(string type)
    {
        Debug.WriteLine("HUI2");
        var f = Collections.Instance.FindTrainingGroup(type);
        if (f == null) return;
        
        var w = new ItemTrainingPage() { DataContext = this };

        foreach (var i in f.Entity.Trainings)
        {
            var r = new ListTrainingBrick(i.Name ?? "none", i.NameIndex);
            r.Command = ListBrickCommand;
            w.Panel.Children.Add(r);
        }

        _historyPage.Push(_generatePage ?? _trainingPage);
        _generatePage = w;
        
        OnSetPage?.Invoke(_generatePage);
    }

    private void OpenListBrickPage(string type)
    {
        var f = Collections.Instance.FindTraining(type);
        if (f == null) return;

        BitmapImage? image = null;
        if (f.Entity.Image != null)
        {
            try
            {
                var bytes = Convert.FromBase64String(f.Entity.Image);
                image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = new MemoryStream(bytes);
                image.EndInit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        var w = new ContentTrainingPage(BackCommand, "Описание:", "Техника выполнения:", f.Entity.Description, f.Entity.TechniqueOfExecution, image);
        
        _historyPage.Push(_generatePage!);
        _generatePage = w;
        
        OnSetPage?.Invoke(_generatePage);
    }

    private void Back(object? obj)
    {
        Debug.WriteLine(_historyPage.Count);
        if (!_historyPage.TryPop(out var result))
        {
            _generatePage = null;
            OnSetPage?.Invoke(_trainingPage);
            return;
        }
        
        _generatePage = result;
        OnSetPage?.Invoke(_generatePage);
    }
}

// _trainingBricks = (from UIElement child in _trainingPage.MainBox.Children select (child as TrainingBrick)!).ToList();