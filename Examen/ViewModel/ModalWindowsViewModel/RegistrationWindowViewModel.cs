using DataLayer.Services;
using Model;
using System.Windows;
using System.Windows.Input;
using ViewModel.Core;
using ViewModel.Services.Interfaces;

namespace ViewModel.ModalWindowsViewModel
{
    public class RegistrationWindowViewModel : BasePageViewModel, IClosable
    {
        public RegistrationWindowViewModel() { } // Нужен для дизайнера
        private readonly Service _humanService;
        public ICommand SendFormRegistration { get; }
        public event Action? RequestClose;

        private string _login;
        private string _password;
        private string _name;
        private string _surname;
        private string _patronymic;
        private string _mail;
        private string _phoneNumber;
        public RegistrationWindowViewModel(Service humanService)
        {
            _humanService = humanService;

            SendFormRegistration = new RelayCommand(OnSendForm);
        }

        private string _humanType;
        public void GetTypeHuman(string humanType)
        {
            _humanType = humanType;
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

            HumansModel human;
            if (_humanType == "user")
            {
                human = new UsersModel
                (
                    Login,
                    Password,
                    Name,
                    Surname,
                    Patronymic,
                    Mail,
                    PhoneNumber
                );
            }
            else
            {
                human = new AdminsModel
                (
                    Login,
                    Password,
                    Name,
                    Surname,
                    Patronymic,
                    Mail,
                    PhoneNumber
                );
            }                

            try
            {
                int newId = await _humanService.AddHumanAsync(human);

                if (newId > 0)
                {
                    MessageBox.Show($"Регистрация успешна! ID: {newId}");
                    RequestClose?.Invoke();
                }
                else
                {
                    MessageBox.Show("Ошибка при добавлении.");
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
}