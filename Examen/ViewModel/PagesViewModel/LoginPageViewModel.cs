using DataLayer.Services;
using Model.Models;
using System.Windows;
using System.Windows.Input;
using ViewModel.Core;
using ViewModel.Services.Interfaces;

namespace ViewModel.PagesViewModel;
public class LoginPageViewModel : BasePageViewModel
{
    public LoginPageViewModel() { } // Нужен для дизайнера
    private readonly INavigateService _navigateService;
    private readonly UserService _userService;
    public ICommand NavigateToSignIn { get; }
    public ICommand NavigateToRegistration { get; }
    public LoginPageViewModel(INavigateService navigateService, UserService userService)
    {
        _navigateService = navigateService;
        _userService = userService;

        NavigateToSignIn = new RelayCommand(OnLoginAsync);
        NavigateToRegistration = new RelayCommand(obj => navigateService.NavigateTo<RegistrationPageViewModel>());
    }
    private string _login;
    private string _password;
    public string Login
    {
        get => _login;
        set => Set(ref _login, value);
    }
    public string Password
    {
        get => _password;
        set => Set(ref _password, value);
    }
    private async void OnLoginAsync(object? obj)
    {
        if (string.IsNullOrWhiteSpace(Login) ||
            string.IsNullOrWhiteSpace(Password))
        {
            MessageBox.Show("Заполните обязательные поля.");
            return;
        }

        var user = new UsersModel
        (
            Login,
            Password
        );

        try
        {
            bool result = await _userService.OnLoginAsync(user);

            if (result)
            {
                MessageBox.Show($"Авторизация успешна!");
                _navigateService.NavigateTo<UserPageViewModel>();
            }
            else
            {
                MessageBox.Show("Ошибка при авторизации.");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка: " + ex.Message);
        }
    }
}