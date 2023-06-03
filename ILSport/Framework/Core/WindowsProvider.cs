using System;
using System.Collections.Generic;
using System.Windows;
using ILSport.Windows;

namespace ILSport.Framework.Core;

public class WindowsProvider
{
    
    private static WindowsProvider? _instance;

    public static WindowsProvider Instance => _instance ??= new WindowsProvider();
    
    private readonly MainWindow _mainWindow;
    private readonly LoginWindow _loginWindow;
    private readonly RegisterWindow _registerWindow;

    private Queue<WindowType> _windowsTypes = new Queue<WindowType>();

    public MainWindow MainWindow => _mainWindow;
    public LoginWindow LoginWindow => _loginWindow;
    public RegisterWindow RegisterWindow => _registerWindow;

    private WindowsProvider()
    {
        _mainWindow = new MainWindow();
        _loginWindow = new LoginWindow();
        _registerWindow = new RegisterWindow();
    }

    public bool Switch(WindowType windowType)
    {
        switch (windowType)
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
    Register
}