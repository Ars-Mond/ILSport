using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ILSport.Custom;

public partial class ListTrainingBrick : UserControl
{
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(nameof(Text), typeof(string), typeof(ListTrainingBrick), new PropertyMetadata(string.Empty));
    
    public static readonly DependencyProperty TypeProperty =
        DependencyProperty.Register(nameof(Type), typeof(string), typeof(ListTrainingBrick), new PropertyMetadata(string.Empty));

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    
    public string Type
    {
        get => (string)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    public ICommand Command { get; set; }

   

    public ListTrainingBrick(string text, string type)
    {
        InitializeComponent();
        DataContext = this;

        Text = text;
        Type = type;
    }

    public ListTrainingBrick()
    {
        InitializeComponent();
        DataContext = this;
    }
}