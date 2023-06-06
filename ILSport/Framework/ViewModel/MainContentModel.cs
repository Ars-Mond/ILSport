using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using ILSport.Custom;
using ILSport.Framework.Core;
using ILSport.Framework.Core.Schemas;
using ILSport.Framework.ViewModel.Controllers;
using ILSport.Pages;
using MenuItem = ILSport.Custom.MenuItem;

namespace ILSport.Framework.ViewModel;

public class MainContentModel : BaseViewModel
{
    private Frame _frame;
    private ICommand PageCommand { get; }
    private List<MenuItem> _menuItems = new List<MenuItem>();
    private List<IPageController> _pageControllers = new List<IPageController>();

    public ICommand BackCommand { get; }

    public MainContentModel(UserSchema user, Frame content)
    {
        _frame = content;
        
        var tg = Collections.Instance.GetTrainingGroupObservableCollection();
        var t = Collections.Instance.GetTrainingObservableCollection();

        AddPageController(new StartupPageController());
        AddPageController(new TrainingPageController(tg, t));
        AddPageController(new ProgressPageController());
        AddPageController(new ProfilePageController());

        BackCommand = new DelegateCommand(Back);
        PageCommand = new DelegateCommand(o => SwitchPage((MenuItem)o!));
        
        Init();
    }

    public void Init()
    {
        var f = _menuItems.Find(i => i.ParameterType == MenuItemType.Home);
        if (f == null) return;
        
        SwitchPage(f);
    }

    public void AddMenuItem(MenuItem item)
    {
        _menuItems.Add(item);
        item.Command = PageCommand;
    }

    private void AddPageController(IPageController pageController)
    {
        _pageControllers.Add(pageController);
        pageController.OnSetPage += OpenPage;
    }

    private void SwitchPage(MenuItem obj)
    {
        var f = _pageControllers.Find(i => i.MenuItemType == obj.ParameterType);

        if (f != null)
        {
            OpenPage(f.GetPage());
            foreach (var item in _menuItems) item.IsActive = false;
            obj.IsActive = true;
        };
    }

    private void OpenPage(Page page)
    {
        _frame.Content = page;
    }

    private void Back(object? obj)
    {
        WindowsProvider.Instance.BackStateFromHistory(true);
    }
}