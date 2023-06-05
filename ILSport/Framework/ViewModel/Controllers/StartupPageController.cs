using System;
using System.Windows.Controls;
using ILSport.Custom;
using ILSport.Pages;

namespace ILSport.Framework.ViewModel.Controllers;

public class StartupPageController : IPageController
{
    private readonly StartupPage _startupPage;
    
    
    public event Action<Page>? OnSetPage;
    public MenuItemType MenuItemType { get; } = MenuItemType.Home;
    public Page GetPage()
    {
        return _startupPage;
    }

    public StartupPageController()
    {
        _startupPage = new StartupPage();
    }
}