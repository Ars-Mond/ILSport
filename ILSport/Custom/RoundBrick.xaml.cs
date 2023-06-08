using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ILSport.Custom
{
    public partial class RoundBrick : UserControl
    {
        public static readonly DependencyProperty ImagePathProperty =
            DependencyProperty.Register(nameof(ImagePath), typeof(ImageSource), typeof(RoundBrick));
        
        public ImageSource ImagePath
        {
            get => (ImageSource)GetValue(ImagePathProperty);
            set => SetValue(ImagePathProperty, value);
        }
        public int Value { get; set; }
    
        public int MaxValue { get; set; }

        public int Data => (int)Math.Round((double)Value / MaxValue * 100d, MidpointRounding.AwayFromZero);
        public RoundBrick()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}