using System.Windows.Controls;

namespace ILSport.Custom;

public partial class ListDataBrick : UserControl
{
    public string Type { get; set; }
    public string Value { get; set; }
    public string Date { get; set; }
    
    public ListDataBrick()
    {
        InitializeComponent();
        DataContext = this;
        
        if (string.IsNullOrEmpty(Type)) Type = "Type";
        if (string.IsNullOrEmpty(Value)) Value = "228,48";
        if (string.IsNullOrEmpty(Date)) Date = "32.12.2077";
    }
}