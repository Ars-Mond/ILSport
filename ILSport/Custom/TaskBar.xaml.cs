using System;
using System.Windows;
using System.Windows.Controls;

namespace ILSport.Custom;

public partial class TaskBar : UserControl
{
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