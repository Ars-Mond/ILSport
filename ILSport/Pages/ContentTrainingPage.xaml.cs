using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ILSport.Pages
{
    public partial class ContentTrainingPage : Page
    {
    
        public ICommand BackCommand { get;}
        public string? TitleText { get; set; }
        public string? TitleText2 { get; set; }
        public string? ContentText { get; set; }
        public string? ContentText2 { get; set; }

        public BitmapImage? Image { get; set; }
    
    
        public ContentTrainingPage(ICommand backCommand, string? title, string? title2, string? content, string? content2, BitmapImage? image)
        {
            BackCommand = backCommand;
            TitleText = title;
            TitleText2 = title2;
            ContentText = content;
            ContentText2 = content2;

            Image = image;
        
            InitializeComponent();
            DataContext = this;
        }
    
        public ContentTrainingPage()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}