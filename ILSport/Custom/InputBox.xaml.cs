using System.Windows.Controls;

namespace ILSport.Custom
{
    public partial class InputBox : UserControl
    {
        public string Title { get; set; }
        public string Value { get; set; }
        public bool IsEdit { get; set; }
        public InputBox()
        {
            InitializeComponent();
            DataContext = this;

            if (string.IsNullOrEmpty(Title)) Title = "Title";
            if (string.IsNullOrEmpty(Value)) Value = "Value";
        }
    }
}