using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
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
            //Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
            var path = $@"{AppDomain.CurrentDomain.BaseDirectory}\ILSport.db";
            Debug.WriteLine(path);
            if (File.Exists(path))
            {
                File.Delete(path);
                Debug.WriteLine("Deleted BD!");
            }

            WindowsProvider = WindowsProvider.Instance;
            Collections = Collections.Instance;
        }
        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            WindowsProvider.Switch(WindowType.Login);
        }
    }
}