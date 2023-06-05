using System;
using System.Windows.Controls;
using ILSport.Custom;
using ILSport.Pages;

namespace ILSport.Framework.ViewModel.Controllers;

public class ProgressPageController : IPageController
{
    private readonly ProgressPage _progressPage;
    
    
    public event Action<Page>? OnSetPage;
    public MenuItemType MenuItemType { get; } = MenuItemType.Progress;
    public Page GetPage()
    {
        return _progressPage;
    }

    public ProgressPageController()
    {
        _progressPage = new ProgressPage();
    }
}