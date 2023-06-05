using System;
using System.Windows.Controls;
using ILSport.Custom;
using ILSport.Pages;

namespace ILSport.Framework.ViewModel.Controllers;

public class ProfilePageController : IPageController
{
    private readonly ProfilePage _profilePage;
    
    
    public event Action<Page>? OnSetPage;
    public MenuItemType MenuItemType { get; } = MenuItemType.Profile;
    public Page GetPage()
    {
        return _profilePage;
    }

    public ProfilePageController()
    {
        _profilePage = new ProfilePage();
    }
}