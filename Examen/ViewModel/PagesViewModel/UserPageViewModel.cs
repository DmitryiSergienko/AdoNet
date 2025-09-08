using DataLayer.Procedures;
using DataLayer.Services;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ViewModel.Core;
using ViewModel.Services.Interfaces;

namespace ViewModel.PagesViewModel;
public class UserPageViewModel : BasePageViewModel
{
    public UserPageViewModel() { } // Нужен для дизайнера
    private readonly INavigateService _navigateService;
    private readonly Service _service;
    private readonly DataLayer.Models.DataBaseContext _dbContext;
    public string? LoginUser => _service.CurrentHuman?.Login;
    public string? NameUser => _service.CurrentHuman?.Name;
    public string? SurnameUser => _service.CurrentHuman?.Surname;
    public string? EmailUser => _service.CurrentHuman?.Mail;
    public string? PhoneUser => _service.CurrentHuman?.PhoneNumber;
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
    public UserPageViewModel(INavigateService navigateService, Service service, DataLayer.Models.DataBaseContext dbContext)
    {
        _navigateService = navigateService;
        _service = service;
        _dbContext = dbContext;

        LogOut = new RelayCommand(obj =>
        {
            IsPortionsFormVisible = Visibility.Collapsed;
            service.LogOut();
            navigateService.NavigateTo<LoginPageViewModel>();
        });
        ShowTop3Products = new AsyncRelayCommand(async () =>
        {
            try
            {
                IsPortionsFormVisible = Visibility.Collapsed;
                var result = await _service.GetTop3ProductsAsync();
                Data = await AddImageColumnAsync(result);
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
        EnterCountPortions = new AsyncRelayCommand(async () =>
        {
            try
            {
                var result = await _service.GetShowProductsInPortionsAsync(_skipRows, CountPortions);
                Data = await AddImageColumnAsync(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        });
        LeftArrowCountPortions = new AsyncRelayCommand(async () =>
        {
            try
            {
                if (_skipRows > 0)
                {
                    _skipRows -= CountPortions;
                    if (_skipRows < 0) _skipRows = 0;
                }
                var result = await _service.GetShowProductsInPortionsAsync(_skipRows, CountPortions);
                Data = await AddImageColumnAsync(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        });
        RightArrowCountPortions = new AsyncRelayCommand(async () =>
        {
            try
            {
                var products = await _service.GetListAllProductsAsync();
                var totalCount = products.Count;
                var maxSkipRows = (totalCount / CountPortions) * CountPortions;
                if (_skipRows + CountPortions < totalCount)
                {
                    _skipRows += CountPortions;
                    var result = await _service.GetShowProductsInPortionsAsync(_skipRows, CountPortions);
                    Data = await AddImageColumnAsync(result);
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
        ShowUserOrderHistory = new AsyncRelayCommand(async () =>
        {
            try
            {
                IsPortionsFormVisible = Visibility.Collapsed;
                var result = await _service.GetUserOrderHistoryAsync();
                Data = await AddImageColumnAsync(result);
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

    private byte[] _imageData;
    public byte[] ImageData
    {
        get => _imageData;
        set
        {
            if (Set(ref _imageData, value))
            {
                OnPropertyChanged(nameof(Image));
            }
        }
    }
    public BitmapImage Image
    {
        get
        {
            if (_imageData == null || _imageData.Length == 0)
                return null;

            try
            {
                var image = new BitmapImage();
                using (var stream = new MemoryStream(_imageData))
                {
                    image.BeginInit();
                    image.StreamSource = stream;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.EndInit();
                    image.Freeze();
                }
                return image;
            }
            catch
            {
                return null;
            }
        }
    }
    private async Task<DataTable> AddImageColumnAsync(DataTable table)
    {
        // Проверяем, есть ли в таблице столбец "images_id"
        if (!table.Columns.Contains("images_id"))
        {
            // Если столбца нет — просто возвращаем исходную таблицу. НИЧЕГО НЕ ЛОМАЕМ.
            return table;
        }

        try
        {
            // Добавляем новый столбец для хранения данных изображения
            table.Columns.Add("Image", typeof(byte[]));

            // Проходим по каждой строке и заполняем столбец "Image"
            foreach (DataRow row in table.Rows)
            {
                if (row["images_id"] != DBNull.Value && int.TryParse(row["images_id"].ToString(), out int imageId))
                {
                    var imageEntity = await _dbContext.imagesSets.FindAsync(imageId);
                    row["Image"] = imageEntity?.image; // Присваиваем массив байт или null
                }
                else
                {
                    // Если images_id null или не число — оставляем Image как null
                    row["Image"] = DBNull.Value;
                }
            }

            // Удаляем старый столбец "images_id", чтобы он не отображался
            table.Columns.Remove("images_id");
        }
        catch (Exception ex)
        {
            // Логируем ошибку, но НЕ ПРОПУСКАЕМ исключение!
            Console.WriteLine($"⚠️ Ошибка при добавлении столбца Image: {ex.Message}");
            // Если что-то пошло не так, возвращаем исходную таблицу без изменений
            return table;
        }

        return table;
    }
}