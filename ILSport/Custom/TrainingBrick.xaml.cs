using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ILSport.Custom;

public partial class TrainingBrick : UserControl
{
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(nameof(Text), typeof(string), typeof(TrainingBrick));

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public ICommand Command { get; set; }
    
    public string Type { get; set; }

    //public string Text { get; set; }

    /*public TrainingBrick()
    {
        InitializeComponent();
        DataContext = this;

        if (string.IsNullOrEmpty(Text)) Text = "Template";
    }*/
    
    public TrainingBrick(string text, string type)
    {
        InitializeComponent();
        DataContext = this;

        Text = text;
        Type = type;
    }
}