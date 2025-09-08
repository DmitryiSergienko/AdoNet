using DataLayer.Services;
using Model;
using System.Windows;
using System.Windows.Input;
using ViewModel.Core;
using ViewModel.Services.Interfaces;

namespace ViewModel.PagesViewModel;
public class LoginPageViewModel : BasePageViewModel
{
    public LoginPageViewModel() { } // Нужен для дизайнера
    private readonly INavigateService _navigateService;
    private readonly Service _userService;
    public ICommand NavigateToSignIn { get; }
    public ICommand NavigateToRegistration { get; }
    public LoginPageViewModel(INavigateService navigateService, Service userService)
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
        set
        {
            Set(ref _login, value);
            UpdateSignInButtonState();
        }
    }
    public string Password
    {
        get => _password;
        set
        {
            Set(ref _password, value);
            UpdateSignInButtonState();
        }
    }
    private void UpdateSignInButtonState()
    {
        IsEnabledNavigateToSignIn = !string.IsNullOrWhiteSpace(Login)
                                && !string.IsNullOrWhiteSpace(Password);
    }
    private async void OnLoginAsync(object? obj)
    {
        if (string.IsNullOrWhiteSpace(Login) ||
            string.IsNullOrWhiteSpace(Password))
        {
            MessageBox.Show("Заполните обязательные поля.");
            return;
        }

        HumansModel user = new UsersModel(Login, Password);
        HumansModel admin = new AdminsModel(Login, Password);

        try
        {
            if (await _userService.OnLoginAsync(user))
            {
                _navigateService.NavigateTo<UserPageViewModel>();
            }
            else if(await _userService.OnLoginAsync(admin))
            {
                _navigateService.NavigateTo<AdminPageViewModel>();
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
    
    private bool _isEnabledNavigateToSignIn = false;
    public bool IsEnabledNavigateToSignIn
    {
        get => _isEnabledNavigateToSignIn;
        set
        {
            _isEnabledNavigateToSignIn = value;
            OnPropertyChanged();
        }
    }
}