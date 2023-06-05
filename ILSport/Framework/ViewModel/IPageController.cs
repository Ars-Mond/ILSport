using System;
using System.Windows.Controls;
using ILSport.Custom;

namespace ILSport.Framework.ViewModel;

public interface IPageController
{
    public event Action<Page> OnSetPage;
    public MenuItemType MenuItemType { get; }
    public Page GetPage();
}