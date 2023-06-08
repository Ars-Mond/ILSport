using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ILSport.Custom
{
    public partial class TaskBar : UserControl
    {
        
        public static readonly DependencyProperty ImagePathProperty =
            DependencyProperty.Register(nameof(ImagePath), typeof(ImageSource), typeof(TaskBar));
        
        public ImageSource ImagePath
        {
            get => (ImageSource)GetValue(ImagePathProperty);
            set => SetValue(ImagePathProperty, value);
        }
        public string Text { get; set; }
        public double Value { get; set; }
    
        private ColorTaskBar _typeColorBar;
        public ColorTaskBar TypeColorBar
        {
            get => _typeColorBar;
            set
            {
                _typeColorBar = value;

                ProgressBar.Style = _typeColorBar switch
                {
                    ColorTaskBar.Red => Resources["TaskBar.ProcessBar.Red"] as Style,
                    ColorTaskBar.Yellow => Resources["TaskBar.ProcessBar.Yellow"] as Style,
                    ColorTaskBar.Blue => Resources["TaskBar.ProcessBar.Blue"] as Style,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }

        public TaskBar()
        {
            InitializeComponent();
            DataContext = this;
        
            TypeColorBar = ColorTaskBar.Red;

            if (string.IsNullOrEmpty(Text)) Text = "Template";
        }
    }

    public enum ColorTaskBar
    {
        Red,
        Yellow,
        Blue
    }
}