using DataLayer.Services;
using Model;
using System.Windows;
using System.Windows.Input;
using ViewModel.Core;
using ViewModel.Services.Interfaces;

namespace ViewModel.ModalWindowsViewModel
{
    public class DeleteHumanWindowViewModel : BasePageViewModel, IClosable
    {
        public DeleteHumanWindowViewModel() { } // Нужен для дизайнера
        private Service _deleteHum;
        public Service DeleteHum
        {
            get => _deleteHum;
            set => _deleteHum = value;
        }
        public event Action? RequestClose;
        public ICommand SearchHuman { get; set; }
        public ICommand DeleteHuman { get; set; }

        public int Id 
        {
            get => _id;
            set
            {
                Set(ref _id, value);
                UpdateSearchButtonState();
            }
        }
        private int _id;
        public string? Login => _deleteHum?.DeleteHuman?.Login;
        public string? Name => _deleteHum?.DeleteHuman?.Name;
        public string? Surname => _deleteHum?.DeleteHuman?.Surname;
        public string? Patronymic => _deleteHum?.DeleteHuman?.Patronymic;
        public string? Email => _deleteHum?.DeleteHuman?.Mail;
        public string? Phone => _deleteHum?.DeleteHuman?.PhoneNumber;

        private string _humanType;
        public string HumanType
        {
            get => _humanType;
            set => _humanType = value;
        }

        public DeleteHumanWindowViewModel(Service humanService) 
        {
            _deleteHum = humanService;
            RequestClose += OnWindowClosed;

            SearchHuman = new RelayCommand(async obj =>
            {
                try
                {
                    HumansModel human = await humanService.GetHumanInfoById(Id, HumanType);
                    if (human != null)
                    {
                        RefreshHumanProperties();
                        UpdateDeleteButtonState();
                    }
                    else
                    {
                        MessageBox.Show("Пользователь не найден");
                        RefreshHumanProperties();
                        UpdateDeleteButtonState();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            });

            DeleteHuman = new RelayCommand(async obj =>
            {
                MessageBox.Show("Пользователь удалён");

                // ОЧИСТИТЬ ДАННЫЕ ПЕРЕД ОБНОВЛЕНИЕМ UI!
                if (_deleteHum != null)
                {
                    _deleteHum.DeleteHuman = null; // ← ВАЖНО!
                }

                RefreshHumanProperties(); // ← Теперь UI увидит null и очистит поля
                RequestClose?.Invoke();
            });
        }
        private void OnWindowClosed()
        {
            DeleteHum = null;
            RefreshHumanProperties();
        }
        private void UpdateSearchButtonState()
        {
            IsEnabledIdInput = Id > 0;
        }
        private bool _isEnabledIdInput = false;
        public bool IsEnabledIdInput
        {
            get => _isEnabledIdInput;
            set
            {
                _isEnabledIdInput = value;
                OnPropertyChanged();
            }
        }
        private void UpdateDeleteButtonState()
        {
            IsEnabledFormDelete = !string.IsNullOrWhiteSpace(Login)
                                        && !string.IsNullOrWhiteSpace(Name)
                                        && !string.IsNullOrWhiteSpace(Surname)
                                        && !string.IsNullOrWhiteSpace(Patronymic)
                                        && !string.IsNullOrWhiteSpace(Email)
                                        && !string.IsNullOrWhiteSpace(Phone);
        }
        private bool _isEnabledFormDelete = false;

        public bool IsEnabledFormDelete
        {
            get => _isEnabledFormDelete;
            set
            {
                _isEnabledFormDelete = value;
                OnPropertyChanged();
            }
        }
        private void RefreshHumanProperties()
        {
            OnPropertyChanged(nameof(Login));
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Surname));
            OnPropertyChanged(nameof(Patronymic));
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(Phone));
        }
        public void ClearData()
        {
            if (_deleteHum != null)
            {
                _deleteHum.DeleteHuman = null;
            }
            Id = 0;
            RefreshHumanProperties();
        }
    }
}