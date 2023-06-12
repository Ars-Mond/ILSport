using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using ILSport.Framework;

namespace ILSport.Windows.MessageBoxes
{
    public partial class ImageBase64Window : Window, INotifyPropertyChanged
    {
        public ICommand OkCommand { get; set; }
        public ICommand SelectCommand { get; set; }
        public string PathImage { get; set; }
        public string HashImage { get; set; }
        
        public ImageBase64Window()
        {
            InitializeComponent();
            DataContext = this;

            OkCommand = new DelegateCommand(Ok);
            SelectCommand = new DelegateCommand(Select);
        }

        private void Ok(object? obj)
        {
            DialogResult = true;
        }

        private void Select(object? obj)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog()
            {
                FileName = "Image",
                DefaultExt = ".png",
                Filter = "Image (.png)|*.png"
            };

            bool? result = dialog.ShowDialog();

            if (result != true) return;
        
            var path = dialog.FileName;
            FileInfo fi = new FileInfo(path);
        
            if (fi.Length >= 2097152)
            {
                MessageBox.Show("Размер изображения больше 2Мб.", "Выбор фото", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var base64 = Convert.ToBase64String(File.ReadAllBytes(path));
                Debug.WriteLine(base64);
                PathImage = path;
                HashImage = base64;
            }
            catch (Exception e)
            {
                MessageBox.Show("Неизвестная ошибка.", "Выбор фото", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine(e);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}