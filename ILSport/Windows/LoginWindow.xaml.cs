using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ILSport.Framework;
using ILSport.Framework.Core;
using ILSport.Framework.Core.Schemas;
using ILSport.Framework.ViewModel;

namespace ILSport.Windows
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new LoginModel(ErrorBox, LoginInput, PasswordInput);
        }
    }
}