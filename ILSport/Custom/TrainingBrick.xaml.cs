using System.Windows.Controls;

namespace ILSport.Custom;

public partial class TrainingBrick : UserControl
{
    public string Text { get; set; }
    
    public TrainingBrick()
    {
        InitializeComponent();
        DataContext = this;

        if (string.IsNullOrEmpty(Text)) Text = "Template";
    }
}