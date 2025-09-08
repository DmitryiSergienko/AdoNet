using DataLayer.Services;
using Microsoft.Data.SqlClient;
using System.Data;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ViewModel.Core;
using ViewModel.ModalWindowsViewModel;
using ViewModel.Services.Classes;
using ViewModel.Services.Interfaces;

namespace ViewModel.PagesViewModel;
public class AdminPageViewModel : BasePageViewModel
{
    public AdminPageViewModel() { } // Нужен для дизайнера

    private readonly INavigateService _navigateService;
    private readonly Service _service;
    private readonly IDialogService _iDialogService;
    private readonly DataLayer.Models.DataBaseContext _dbContext;
    public string? LoginAdmin => _service.CurrentHuman?.Login;
    public string? NameAdmin => _service.CurrentHuman?.Name;
    public string? SurnameAdmin => _service.CurrentHuman?.Surname;
    public string? EmailAdmin => _service.CurrentHuman?.Mail;
    public string? PhoneAdmin => _service.CurrentHuman?.PhoneNumber;
    public ICommand LogOut { get; }
    public ICommand AddUser { get; }
    public ICommand UpdateUser { get; }
    public ICommand DeleteUser { get; }
    public ICommand AddAdmin { get; }
    public ICommand UpdateAdmin { get; }
    public ICommand DeleteAdmin { get; }
    public ICommand AddProduct { get; }
    public ICommand UpdateProduct { get; }
    public ICommand DeleteProduct { get; }
    public ICommand AddCategory { get; }
    public ICommand UpdateCategory { get; }
    public ICommand DeleteCategory { get; }
    public ICommand AddQuantityProduct { get; }

    private string _inputArea;
    public string InputArea
    {
        get => _inputArea;
        set
        {
            _inputArea = value;
            OnPropertyChanged();
        }
    }
    private DataTable _dataTableAdmin;
    public DataTable DataTableAdmin
    {
        get => _dataTableAdmin;
        set
        {
            _dataTableAdmin = value;
            OnPropertyChanged();
        }
    }
    public ICommand ExecuteQueryCommand { get; }
    public AdminPageViewModel(INavigateService navigateService, IDialogService iDialogService, Service service, DataLayer.Models.DataBaseContext dbContext)
    {
        _navigateService = navigateService;
        _iDialogService = iDialogService;
        _service = service;
        _dbContext = dbContext;

        LogOut = new RelayCommand(obj =>
        {
            service.LogOut();
            navigateService.NavigateTo<LoginPageViewModel>();
        });

        ExecuteQueryCommand = new RelayCommand(ExecuteQuery);
        DataTableAdmin = new DataTable();

        AddUser = new RelayCommand(async obj =>
        {
            var vm = new RegistrationWindowViewModel(_service);
            vm.GetTypeHuman("user");                            
            _iDialogService.ShowModal(vm);                      
        });
        UpdateUser = new RelayCommand(async obj => { });
        DeleteUser = new RelayCommand(async obj => 
        {
            var vm = new DeleteHumanWindowViewModel(_service);
            vm.HumanType = "user";
            _iDialogService.ShowModal(vm);
        });
        AddAdmin = new RelayCommand(async obj =>
        {
            var vm = new RegistrationWindowViewModel(_service);
            vm.GetTypeHuman("admin");
            _iDialogService.ShowModal(vm);
        });
        UpdateAdmin = new RelayCommand(async obj => { });
        DeleteAdmin = new RelayCommand(async obj => 
        {
            var vm = new DeleteHumanWindowViewModel(_service);
            vm.HumanType = "admin";
            _iDialogService.ShowModal(vm);
        });
        AddProduct = new RelayCommand(async obj => { });
        UpdateProduct = new RelayCommand(async obj => { });
        DeleteProduct = new RelayCommand(async obj => { });
        AddCategory = new RelayCommand(async obj => { });
        UpdateCategory = new RelayCommand(async obj => { });
        DeleteCategory = new RelayCommand(async obj => { });
        AddQuantityProduct = new RelayCommand(async obj => { });
    }
    private async void ExecuteQuery(object parameter)
    {
        try
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Examen_ModelFirst;User Id=user1;Password=sa;TrustServerCertificate=True;";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(InputArea, connection))
                using (var adapter = new SqlDataAdapter(command))
                {
                    var resultTable = new DataTable();
                    adapter.Fill(resultTable);

                    // Проверяем, есть ли в таблице столбец "images_id"
                    if (resultTable.Columns.Contains("images_id"))
                    {
                        // Добавляем новый столбец для хранения данных изображения
                        resultTable.Columns.Add("Image", typeof(byte[]));

                        // Проходим по каждой строке и заполняем столбец "Image"
                        foreach (DataRow row in resultTable.Rows)
                        {
                            if (row["images_id"] != DBNull.Value && int.TryParse(row["images_id"].ToString(), out int imageId))
                            {
                                // Используем _dbContext для поиска изображения
                                var imageEntity = await _dbContext.imagesSets.FindAsync(imageId);
                                row["Image"] = imageEntity?.image; // Присваиваем массив байт или null
                            }
                        }

                        // Удаляем старый столбец "images_id", чтобы он не отображался
                        resultTable.Columns.Remove("images_id");
                    }

                    DataTableAdmin = resultTable;
                }
            }
        }
        catch (Exception ex)
        {
            var errorTable = new DataTable();
            errorTable.Columns.Add("Ошибка", typeof(string));
            errorTable.Rows.Add(ex.Message);
            DataTableAdmin = errorTable;
        }
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
}