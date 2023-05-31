using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace ILSport.CustomElements;

public class GapWrapPanel : Canvas
{
    public static readonly DependencyProperty GapProperty = DependencyProperty.Register(nameof(Gap),
        typeof(double),
        typeof(GapWrapPanel),
        new FrameworkPropertyMetadata(
            default(double),
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnGapChanged));

    private static void OnGapChanged(DependencyObject @object, DependencyPropertyChangedEventArgs e)
    {
        ((GapWrapPanel)@object).SetGap();
    }

    public double Gap
    {
        get => (double)GetValue(GapProperty);
        set => SetValue(GapProperty, value);
    }

    public GapWrapPanel()
    {
        LayoutUpdated += (sender, args) =>
        {
            SetGap();
        };
    }

    private void SetGap()
    {
        FrameworkElement[] elements = (from UIElement child in Children select (child as FrameworkElement)!).ToArray();

        double h = 0;

        while (elements.Length != 0)
        {
            var e = GetItemsInnerCanvas(elements, ActualWidth);

            double w = 0;

            foreach (var element in e)
            {
                SetTop(element, h);
                SetLeft(element, w);
                w += element.ActualWidth + Gap;
                
            }
            
            h += GetMaxHeight(e) + Gap;

            elements = elements.Skip(e.Length).ToArray();
        }

        h -= Gap;
        Height = h < 0 ? Double.NaN : h;
    }

    private double GetMaxHeight(FrameworkElement[] elements)
    {
        return elements.Select(element => element.ActualHeight).Prepend(0).Max();
    }

    private FrameworkElement[] GetItemsInnerCanvas(FrameworkElement[] elements, double width)
    {
        List<FrameworkElement> temp = new List<FrameworkElement>();

        foreach (var element in elements)
        {
            var tempWidth = temp.Sum(item => item.ActualWidth + Gap);

            if (tempWidth + element.ActualWidth > width) return temp.ToArray();

            temp.Add(element);
        }

        return elements;
    }
}