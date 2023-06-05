using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ILSport.Framework;

namespace ILSport.Custom;

public partial class MenuItem : UserControl
{
    
    /*public static readonly DependencyProperty MyClickProperty =
        DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(MenuItem));

    public ICommand Command
    {
        get => (ICommand)GetValue(MyClickProperty);
        set => SetValue(MyClickProperty, value);
    }*/
    
    public ICommand Command { get; set; }
    public MenuItemType ParameterType { get; set; } = MenuItemType.None;
    public string Text { get; set; }
    public bool IsActive
    {
        get => _isActive;
        set
        {
            _isActive = value;
            
            if (IsActive) MenuItemButton.Style = Resources["MenuItem.ButtonActive"] as Style; 
            //MenuItemButton.Background = Resources["ColorBrush.Primary"] as LinearGradientBrush;
            
            else MenuItemButton.Style = Resources["MenuItem.Button"] as Style; 
            //MenuItemButton.Background = Resources["ColorBrush.Primary2"] as Brush;
        }
    }
    
    private bool _isActive = false;
    
    public MenuItem This { get; }

    public MenuItem()
    {
        This = this;
        InitializeComponent();
        DataContext = this;
        
        if (string.IsNullOrEmpty(Text)) Text = "Template";
    }
}