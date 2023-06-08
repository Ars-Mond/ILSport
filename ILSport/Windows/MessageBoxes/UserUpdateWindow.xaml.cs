using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ILSport.Framework;

namespace ILSport.Windows.MessageBoxes
{
    public partial class UserUpdateWindow : Window, INotifyPropertyChanged
    {
        public ICommand SelectPhotoCommand { get; }
        public ICommand OkCommand { get; } = null!;
        public ICommand CancelCommand { get; } = null!;

        public string? FamilyName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
    
        public double? Weight { get; set; }
        public string? Birthday { get; set; }
    
        public string? Photo { get; set; }
        public string? PhotoPath { get; set; }
    
        public UserUpdateWindow()
        {
            InitializeComponent();
            DataContext = this;

            SelectPhotoCommand = new DelegateCommand(SelectPhoto);
            OkCommand = new DelegateCommand(Ok);
            CancelCommand = new DelegateCommand(Cancel);
        }

        private void Ok(object? obj)
        {
            SetStateError(false, "Error.");
            if (!CheckString(FamilyName))
            {
                SetStateError(true, "Error login.");
                return;
            }
            if (!CheckString(FirstName))
            {
                SetStateError(true, "Error login.");
                return;
            }
        
            DialogResult = true;
        }

        private void Cancel(object? obj)
        {
            DialogResult = false;
        }

        private void SelectPhoto(object? obj)
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
                Photo = base64;
                PhotoPath = path;
            }
            catch (Exception e)
            {
                MessageBox.Show("Неизвестная ошибка.", "Выбор фото", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine(e);
            }
        }
    
        private bool CheckString(string str)
        {
            return str.Split(new[] { ' ', '_' }).Length == 1 && !string.IsNullOrEmpty(str);
        }
    
        private void SetStateError(bool visible, string text)
        {
            ErrorTextBox.Visibility = visible ? Visibility.Visible : Visibility.Hidden;

            ErrorTextBox.Text = text;
        }
    }
}