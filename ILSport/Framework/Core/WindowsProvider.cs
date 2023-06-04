using System;
using System.Collections.Generic;
using System.Windows;
using ILSport.Framework.Core.Schemas;
using ILSport.Windows;

namespace ILSport.Framework.Core;

public class WindowsProvider
{
    
    private static WindowsProvider? _instance;

    public static WindowsProvider Instance => _instance ??= new WindowsProvider();

    private Window? _openWindow;

    private Queue<WindowType> _windowsTypes = new Queue<WindowType>();

    private WindowsProvider()
    { }

    public bool Switch(WindowType windowType)
    {
        return Switch<byte>(windowType);
    }

    public bool Switch<T>(WindowType windowType, T? data = default)
    {
        Window? window;
        switch (windowType)
        {
            case WindowType.Main:
                if (data is not User user) return false;

                window = new MainWindow(user);
                
                _openWindow?.Close();
                _openWindow = window;
                _openWindow.Show();
                break;
            
            case WindowType.Login:
                window = new LoginWindow();
                
                _openWindow?.Close();
                _openWindow = window;
                _openWindow.Show();
                break;
            
            case WindowType.Register:
                window = new RegisterWindow();
                
                _openWindow?.Close();
                _openWindow = window;
                _openWindow.Show();
                break;
            
            case WindowType.TestData:
                window = new TestDataWindow();
                
                _openWindow?.Close();
                _openWindow = window;
                _openWindow.Show();
                break;
                
            default:
                throw new ArgumentOutOfRangeException(nameof(windowType), windowType, null);
        }
        
        return true;
    }

    public void AddStateToHistory(WindowType windowType)
    {
        _windowsTypes.Enqueue(windowType);
    }

    public bool BackStateFromHistory(bool messagebox = false)
    {
        if (!messagebox) return _windowsTypes.TryDequeue(out var windowType) && Switch(windowType);
        if (MessageBox.Show("Вы уверенны что хотите вернуться?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            return _windowsTypes.TryDequeue(out var windowType) && Switch(windowType);

        return false;
    }
}

public enum WindowType
{
    Main,
    Login,
    Register,
    TestData
}


/*switch (windowType)
        {
            case WindowType.Main:
                _mainWindow.Show();
                _registerWindow.Hide();
                _loginWindow.Hide();
                break;
            case WindowType.Login:
                _loginWindow.Show();
                _registerWindow.Hide();
                _mainWindow.Hide();
                break;
            case WindowType.Register:
                _registerWindow.Show();
                _loginWindow.Hide();
                _mainWindow.Hide();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(windowType), windowType, null);
        }*/
        
/*private readonly MainWindow _mainWindow;
private readonly LoginWindow _loginWindow;
private readonly RegisterWindow _registerWindow;*/

/*public MainWindow MainWindow => _mainWindow;
public LoginWindow LoginWindow => _loginWindow;
public RegisterWindow RegisterWindow => _registerWindow;*/
    
/*_mainWindow = new MainWindow();
    _loginWindow = new LoginWindow();
    _registerWindow = new RegisterWindow();*/
//_mainWindow.Close();
