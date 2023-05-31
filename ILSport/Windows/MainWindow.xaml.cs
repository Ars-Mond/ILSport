using System;
using System.Windows;
using ILSport.Framework;
using ILSport.Pages;

namespace ILSport
{
    public partial class MainWindow : Window
    {
        private readonly StartupPage _startupPage;
        private readonly TrainingPage _trainingPage;
        private readonly ProgressPage _progressPage;
        private readonly ProfilePage _profilePage;

        public MainWindow()
        {
            InitializeComponent();

            _startupPage = new StartupPage();
            _trainingPage = new TrainingPage();
            _progressPage = new ProgressPage();
            _profilePage = new ProfilePage();
            
            // ExitButton.CloseAppCommand = new ActionCommand(Did);
            
            MenuItemHome.Command = new DelegateCommand(SwitchPage);
            MenuItemTraining.Command = new DelegateCommand(SwitchPage);
            MenuItemProgress.Command = new DelegateCommand(SwitchPage);
            MenuItemProfile.Command = new DelegateCommand(SwitchPage);
            
            MainFrame.Content = _startupPage;
            MenuItemHome.IsActive = true;
        }

        private void SwitchPage(object parameter)
        {
            var type = parameter as string;

            switch (type)
            {
                case "Home":
                    MainFrame.Content = _startupPage;
                    
                    MenuItemHome.IsActive = true;
                    MenuItemTraining.IsActive = false;
                    MenuItemProgress.IsActive = false;
                    MenuItemProfile.IsActive = false;
                    break;
                
                case "Training":
                    MainFrame.Content = _trainingPage;
                    
                    MenuItemHome.IsActive = false;
                    MenuItemTraining.IsActive = true;
                    MenuItemProgress.IsActive = false;
                    MenuItemProfile.IsActive = false;
                    break;
                
                case "Progress":
                    MainFrame.Content = _progressPage;
                    
                    MenuItemHome.IsActive = false;
                    MenuItemTraining.IsActive = false;
                    MenuItemProgress.IsActive = true;
                    MenuItemProfile.IsActive = false;
                    break;
                
                case "Profile":
                    MainFrame.Content = _profilePage;
                    
                    MenuItemHome.IsActive = false;
                    MenuItemTraining.IsActive = false;
                    MenuItemProgress.IsActive = false;
                    MenuItemProfile.IsActive = true;
                    break;
                
                default:
                    break;
            }
            
        }
    }
}