using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ILSport.Framework;
using ILSport.Framework.Core;
using ILSport.Framework.Core.Schemas;
using ILSport.Framework.ViewModel;
using ILSport.Pages;

namespace ILSport
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var user = WindowsProvider.Instance.CurrentUser;
            if (user == null) throw new NullReferenceException(nameof(WindowsProvider.Instance.CurrentUser));
            
            var model = new MainContentModel(user, MainFrame);
            DataContext = model;

            ExitButton.CloseAppCommand = model.BackCommand;
            
            model.AddMenuItem(MenuItemHome);
            model.AddMenuItem(MenuItemTraining);
            model.AddMenuItem(MenuItemProgress);
            model.AddMenuItem(MenuItemProfile);
            
            model.Init();
        }
    }
}