using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ILSport.Framework;

namespace ILSport.Custom;

public partial class MenuItem : UserControl, INotifyPropertyChanged
{
    private bool _isActive = false;
    
    public ICommand Command { get; set; }
    public string ParameterType { get; set; }
    public string Text { get; set; }
    public bool IsActive
    {
        get => _isActive;
        set
        {
            _isActive = value;
            if (IsActive) MenuItemButton.Style = Resources["MenuItem.ButtonActive"] as Style; //MenuItemButton.Background = Resources["ColorBrush.Primary"] as LinearGradientBrush;
            else MenuItemButton.Style = Resources["MenuItem.Button"] as Style; //MenuItemButton.Background = Resources["ColorBrush.Primary2"] as Brush;
        }
    }

    public MenuItem()
    {
        InitializeComponent();
        DataContext = this;
        
        if (string.IsNullOrEmpty(Text)) Text = "Template";
        if (string.IsNullOrEmpty(ParameterType)) ParameterType = "Template";

        Command = new DelegateCommand(Fot);
    }

    private void Fot(object? obj)
    {
    }
}