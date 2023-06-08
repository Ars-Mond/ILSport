using System.Windows;
using System.Windows.Controls;
using System;
using System.Linq;
using System.Windows.Media;

namespace ILSport.CustomElements
{
    public class GapStackPanel : StackPanel
    {
        public static readonly DependencyProperty GapProperty = DependencyProperty.Register(nameof(Gap), 
            typeof(double),
            typeof(GapStackPanel),
            new FrameworkPropertyMetadata(
                default(double),
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnGapChanged));
        public double Gap
        {
            get => (double)GetValue(GapProperty);
            set => SetValue(GapProperty, value);
        }
    
    
        private static void OnGapChanged(DependencyObject @object, DependencyPropertyChangedEventArgs e)
        {
            ((GapStackPanel)@object).SetGap();
        }

        private void SetGap()
        {
            FrameworkElement[] elements = (from UIElement child in Children select (child as FrameworkElement)!).ToArray();
        
            if(elements.Length <= 0) return;

            foreach (var element in elements)
            {
                element.Margin = Orientation == Orientation.Vertical ? new Thickness(0, 0, 0, 0 + Gap) : new Thickness(0, 0, 0 + Gap, 0);
            }

            var el = elements.Last();

            el.Margin = new Thickness(0, 0,0, 0);
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            SetGap();
        }
    }
}