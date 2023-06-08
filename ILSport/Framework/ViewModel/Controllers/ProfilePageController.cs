using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ILSport.Custom;
using ILSport.Framework.Core;
using ILSport.Framework.Core.Schemas;
using ILSport.Pages;
using ILSport.Windows.MessageBoxes;

namespace ILSport.Framework.ViewModel.Controllers
{
    public class ProfilePageController : BaseViewModel, IPageController
    {
        private readonly ProfilePage _profilePage;
        public UserSchema User { get; private set; }
        public string UserFamilyName { get; private set; }
        public string UserFirstName { get; private set; }
        public string UserBirthday { get; private set; }
        public string UserWeight { get; private set; }
        public string UserSleep { get; private set; }
        public BitmapImage? Avatar { get; private set; }

        public ICommand UpdateUserCommand { get; }
    
    
        public UserUpdateWindow? UserWindow { get; private set; }
    
    
        public event Action<Page>? OnSetPage;
        public MenuItemType MenuItemType { get; } = MenuItemType.Profile;
        public Page GetPage()
        {
            return _profilePage;
        }

        public ProfilePageController(UserSchema user)
        {
            User = user;
            UpdateProperty();

            Debug.WriteLine(User.FamilyName);
            _profilePage = new ProfilePage() {DataContext = this};

            UpdateUserCommand = new DelegateCommand(UpdateUser);
        
        }

        private void UpdateUser(object? obj)
        {
            try
            {
                UserWindow = new UserUpdateWindow()
                {
                    FamilyName = User.FamilyName,
                    FirstName = User.FirstName,
                    MiddleName = User.MiddleName,
                    Weight = User.Weight,
                    Birthday = User.Birthday,
                    Photo = User.Avatar,
                    PhotoPath = User.Avatar != null ? "hidden" : "none"
                };
            
                var showDialog = UserWindow.ShowDialog();
                if (showDialog is not true) return;

                User.FamilyName = UserWindow.FamilyName ?? User.FamilyName ?? "";
                User.FirstName = UserWindow.FirstName ?? User.FirstName ?? "";
                User.MiddleName = UserWindow.MiddleName ?? User.MiddleName ?? "";
                User.Weight = UserWindow.Weight ?? 0;
                User.Birthday = UserWindow.Birthday ?? User.Birthday ?? "";

                User.Avatar = UserWindow.Photo ?? User.Avatar ?? "";

                Collections.Instance.DatabaseContext.SaveChanges();
                UpdateProperty();
            }
            catch (Exception e)
            {
                MessageBox.Show("При создании произошла неизвестная ошибка.", "Создание", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine(e);
            }
        }


        private void UpdateProperty()
        {
            UserFamilyName = User.FamilyName ?? "";
            UserFirstName = User.FirstName ?? "";
            UserBirthday =  User.Birthday ?? "";
            UserWeight = (User.Weight ?? 0) + "кг";
            UserSleep = (User.Sleep ?? 0) == 0 ? "0ч 00мин" : User.Sleep + "ч";

            try
            {
                if (string.IsNullOrEmpty(User.Avatar)) return;
            
                var bytes = Convert.FromBase64String(User.Avatar);
                
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = new MemoryStream(bytes);
                bi.EndInit();

                Avatar = bi;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}


//var t = MessageBox.Show("Создание пользователя данным образом устарело. Может вызвать исключение. Уверенны что хотите начать создание?", "Создание", MessageBoxButton.YesNo, MessageBoxImage.Question);
//if (t != MessageBoxResult.Yes) return;