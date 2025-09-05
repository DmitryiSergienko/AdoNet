using DataLayer.Procedures;
using DataLayer.Services;
using System.Data;
using System.Windows;
using System.Windows.Input;
using ViewModel.Core;
using ViewModel.Services.Interfaces;

namespace ViewModel.PagesViewModel;
public class UserPageViewModel : BasePageViewModel
{
    public UserPageViewModel() { } // Нужен для дизайнера
    private readonly INavigateService _navigateService;
    private readonly UserService _userService;
    public string? LoginUser => _userService.CurrentUser?.Login;
    public string? NameUser => _userService.CurrentUser?.Name;
    public string? SurnameUser => _userService.CurrentUser?.Surname;
    public string? EmailUser => _userService.CurrentUser?.Mail;
    public string? PhoneUser => _userService.CurrentUser?.PhoneNumber;
    public ICommand LogOut { get; }
    public ICommand ShowTop3Products { get; }
    public ICommand SearchProductByName { get; }
    public ICommand SearchProductByPrice { get; }
    public ICommand ShowProductInCategory { get; }
    public ICommand ShowProductInPortions { get; }
    public ICommand SearchOrdersByDate { get; }
    public ICommand ShowUserOrderHistory { get; }
    public ICommand CreateOrder { get; }
    public ICommand ShowAllProducts { get; }

    private int _skipRows = 0;
    private int _countPortions;
    public int CountPortions 
    { 
        get => _countPortions;
        set
        {
            Set(ref _countPortions, value);
            UpdateCountPortionsButtonState();
        }
    }
    public ICommand EnterCountPortions { get; }
    public ICommand LeftArrowCountPortions { get; }
    public ICommand RightArrowCountPortions { get; }

    private DataTable _data;
    public DataTable Data
    {
        get => _data;
        set
        {
            _data = value;
            OnPropertyChanged();
        }
    }
    public UserPageViewModel(INavigateService navigateService, UserService userService) 
    {
        _navigateService = navigateService;
        _userService = userService;

        LogOut = new RelayCommand(obj =>
        {
            IsPortionsFormVisible = Visibility.Collapsed;
            userService.LogOut();
            navigateService.NavigateTo<LoginPageViewModel>();
        });
        ShowTop3Products = new RelayCommand(async obj => 
        {
            try
            {
                IsPortionsFormVisible = Visibility.Collapsed;
                var result = await _userService.GetTop3ProductsAsync();
                Data = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        });
        SearchProductByName = new RelayCommand(async obj => 
        {
            IsPortionsFormVisible = Visibility.Collapsed;
        });
        SearchProductByPrice = new RelayCommand(async obj => 
        {
            IsPortionsFormVisible = Visibility.Collapsed;
        });
        ShowProductInCategory = new RelayCommand(async obj => 
        {
            IsPortionsFormVisible = Visibility.Collapsed;
        });
        ShowProductInPortions = new RelayCommand(async obj => 
        {
            try
            {
                IsPortionsFormVisible = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        });
        EnterCountPortions = new RelayCommand(async obj =>
        {
            try
            {
                var result = await _userService.GetShowProductsInPortionsAsync(_skipRows, CountPortions);
                Data = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        });
        LeftArrowCountPortions = new RelayCommand(async obj =>
        {
            try
            {
                if (_skipRows > 0)
                {
                    _skipRows -= CountPortions;
                    if (_skipRows < 0) _skipRows = 0;
                }
                var result = await _userService.GetShowProductsInPortionsAsync(_skipRows, CountPortions);
                Data = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        });
        RightArrowCountPortions = new RelayCommand(async obj =>
        {
            try
            {
                var products = await _userService.GetListAllProductsAsync();
                var totalCount = products.Count;
                var maxSkipRows = (totalCount / CountPortions) * CountPortions;
                if (_skipRows + CountPortions < totalCount)
                {
                    _skipRows += CountPortions;

                    var result = await _userService.GetShowProductsInPortionsAsync(_skipRows, CountPortions);
                    Data = result;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        });
        SearchOrdersByDate = new RelayCommand(async obj => 
        {
            IsPortionsFormVisible = Visibility.Collapsed;
        });
        ShowUserOrderHistory = new RelayCommand(async obj => 
        {
            try
            {
                IsPortionsFormVisible = Visibility.Collapsed;
                var result = await _userService.GetUserOrderHistoryAsync();
                Data = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        });
        CreateOrder = new RelayCommand(async obj => 
        {
            IsPortionsFormVisible = Visibility.Collapsed;
        });
        ShowAllProducts = new RelayCommand(async obj => 
        {
            IsPortionsFormVisible = Visibility.Collapsed;
        });
    }

    private Visibility _isPortionsFormVisible = Visibility.Collapsed;
    public Visibility IsPortionsFormVisible
    {
        get => _isPortionsFormVisible;
        set
        {
            _isPortionsFormVisible = value;
            OnPropertyChanged();
        }
    }

    private bool _enterCountPortionsIsEnabled = false;
    public bool EnterCountPortionsIsEnabled
    {
        get => _enterCountPortionsIsEnabled;
        set
        {
            _enterCountPortionsIsEnabled = value;
            OnPropertyChanged();
        }
    }
    private void UpdateCountPortionsButtonState()
    {
        EnterCountPortionsIsEnabled = CountPortions > 0;
    }
}