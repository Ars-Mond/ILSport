using System;
using System.Windows.Controls;

namespace ILSport.Custom;

public partial class RoundBrick : UserControl
{
    public int Value { get; set; }
    
    public int MaxValue { get; set; }

    public int Data => (int)Math.Round((double)Value / MaxValue * 100d, MidpointRounding.AwayFromZero);
    public RoundBrick()
    {
        InitializeComponent();
        DataContext = this;
    }
}