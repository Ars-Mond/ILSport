using System.Windows;
using System.Windows.Controls;
using System;
using System.Linq;

namespace ILSport.CustomElements;

public class GapStackPanel : StackPanel
{
    public static readonly DependencyProperty GapProperty = DependencyProperty.Register(nameof(Gap), 
        typeof(double),
        typeof(GapStackPanel));
    public double Gap
    {
        get => (double)GetValue(GapProperty);
        set => SetValue(GapProperty, value);
    }
    
    public GapStackPanel()
    {
        LayoutUpdated += (sender, args) => SetPadding();
    }
    private static void OnGapChanged(DependencyObject @object, DependencyPropertyChangedEventArgs e)
    {
        ((GapStackPanel)@object).SetPadding();
    }

    private void SetPadding()
    {
        FrameworkElement[] elements = (from UIElement child in Children select (child as FrameworkElement)!).ToArray();
        
        if(elements.Length <= 0) return;

        foreach (var element in elements)
        {
            element.Margin = new Thickness(0, 0, 0, 0 + Gap);
        }

        var el = elements.Last();

        el.Margin = new Thickness(0, 0,0, 0);
    }
}