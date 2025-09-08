using DataLayer.Services;
using Model;
using System.Windows;
using System.Windows.Input;
using ViewModel.Core;
using ViewModel.Services.Interfaces;

namespace ViewModel.PagesViewModel;
public class RegistrationPageViewModel : BasePageViewModel
{
    public RegistrationPageViewModel() { } // Нужен для дизайнера
    private readonly INavigateService _navigateService;
    private readonly Service _userService;
    public ICommand BackToLoginPage { get; }
    public ICommand SendFormRegistration { get; }

    private string _login;
    private string _password;
    private string _name;
    private string _surname;
    private string _patronymic;
    private string _mail;
    private string _phoneNumber;
    public RegistrationPageViewModel(INavigateService navigateService, Service userService)
    {
        _navigateService = navigateService;
        _userService = userService;

        BackToLoginPage = new RelayCommand(obj => navigateService.NavigateTo<LoginPageViewModel>());
        SendFormRegistration = new RelayCommand(OnSendForm);
    }
    private async void OnSendForm(object? obj)
    {
        if (string.IsNullOrWhiteSpace(Login) ||
            string.IsNullOrWhiteSpace(Password) ||
            string.IsNullOrWhiteSpace(Name) ||
            string.IsNullOrWhiteSpace(Surname) ||
            string.IsNullOrWhiteSpace(Mail))
        {
            MessageBox.Show("Заполните обязательные поля.");
            return;
        }

        var user = new UsersModel
        (
            Login,
            Password,
            Name,
            Surname,
            Patronymic,
            Mail,
            PhoneNumber
        );

        try
        {
            int newId = await _userService.AddHumanAsync(user);

            if (newId > 0)
            {
                MessageBox.Show($"Регистрация успешна! ID: {newId}");
                _navigateService.NavigateTo<LoginPageViewModel>();
            }
            else
            {
                MessageBox.Show("Ошибка при добавлении пользователя.");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка: " + ex.Message);
        }
    }

    public string Login
    {
        get => _login;
        set
        {
            Set(ref _login, value);
            UpdateSendButtonState();
        } 
    }
    public string Password
    {
        get => _password;
        set
        {
            Set(ref _password, value);
            UpdateSendButtonState();
        }
    }
    public string Name
    {
        get => _name;
        set
        {
            Set(ref _name, value);
            UpdateSendButtonState();
        }
    }
    public string Surname
    {
        get => _surname;
        set
        {
            Set(ref _surname, value);
            UpdateSendButtonState();
        }
    }
    public string Patronymic
    {
        get => _patronymic;
        set
        {
            Set(ref _patronymic, value);
            UpdateSendButtonState();
        }
    }
    public string Mail
    {
        get => _mail;
        set
        {
            Set(ref _mail, value);
            UpdateSendButtonState();
        }
    }
    public string PhoneNumber
    {
        get => _phoneNumber;
        set
        {
            Set(ref _phoneNumber, value);
            UpdateSendButtonState();
        }
    }
    private void UpdateSendButtonState()
    {
        IsEnabledSendFormRegistration = !string.IsNullOrWhiteSpace(Login)
                                    && !string.IsNullOrWhiteSpace(Password)
                                    && !string.IsNullOrWhiteSpace(Name)
                                    && !string.IsNullOrWhiteSpace(Surname)
                                    && !string.IsNullOrWhiteSpace(Patronymic)
                                    && !string.IsNullOrWhiteSpace(Mail)
                                    && !string.IsNullOrWhiteSpace(PhoneNumber);
           
    }
    private bool _isEnabledSendFormRegistration = false;
    public bool IsEnabledSendFormRegistration 
    { 
        get => _isEnabledSendFormRegistration;
        set
        {
            _isEnabledSendFormRegistration = value;
            OnPropertyChanged();
        } 
    }
}