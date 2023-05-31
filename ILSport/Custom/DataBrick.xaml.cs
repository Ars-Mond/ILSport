using System.Windows.Controls;

namespace ILSport.Custom;

public partial class DataBrick : UserControl
{
    public string Title { get; set; }
    public string Value { get; set; }
    public string Unit { get; set; }
    public string Date { get; set; }
    
    public DataBrick()
    {
        InitializeComponent();
        DataContext = this;
        if (string.IsNullOrEmpty(Title)) Title = "Template Title";
        if (string.IsNullOrEmpty(Value)) Value = "228,48";
        if (string.IsNullOrEmpty(Unit)) Unit = "км";
        if (string.IsNullOrEmpty(Date)) Date = "32.12.2077";
    }
}