using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ILSport.Framework.Core;
using ILSport.Windows;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ILSport
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private WindowsProvider WindowsProvider { get; }
        private Collections Collections { get; }
        
        public App()
        {
            WindowsProvider = WindowsProvider.Instance;
            Collections = Collections.Instance;

            //Collections.Users.Load();
        }
        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            WindowsProvider.Switch(WindowType.Login);
        }
    }
}