using DataLayer.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using View.Pages;
using ViewModel.ModalWindowsViewModel;
using ViewModel.PagesViewModel;
using ViewModel.Services.Classes;
using ViewModel.Services.Interfaces;

namespace View;

public partial class App : Application
{
    public IServiceProvider Services { get; }

    public App()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        Services = services.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Examen_ModelFirst;Trusted_Connection=true;TrustServerCertificate=true;";
        services.AddDbContext<DataLayer.Models.DataBaseContext>(options =>
        options.UseSqlServer(connectionString));

        services.AddDbContext<DataLayer.Procedures.DataBaseContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddDbContext<DataLayer.Views.DataBaseContext>(options =>
            options.UseSqlServer(connectionString));
        services.AddScoped<Service>();
        services.AddSingleton<INavigateService, NavigateService>();
        services.AddTransient<LoginPageViewModel>();
        services.AddTransient<RegistrationPageViewModel>();
        services.AddTransient<AdminPageViewModel>();
        services.AddTransient<UserPageViewModel>();
        services.AddSingleton<IDialogService, DialogService>();
        services.AddTransient<RegistrationWindowViewModel>();

        services.AddDbContext<DataLayer.Models.DataBaseContext>(options =>
    options.UseSqlServer(connectionString));
    }
    protected override async void OnStartup(StartupEventArgs e)
    {
        try
        {
            base.OnStartup(e);

            var mainWindow = new ContainerWindow();
            var navigateService = Services.GetService<INavigateService>();

            if (navigateService == null)
                throw new InvalidOperationException("NavigateService не зарегистрирован в DI.");

            navigateService.ConfigureNavigation(mainWindow.MainFrame);

            navigateService.Navigated += (pageName) =>
            {
                Page? page = pageName switch
                {
                    "LoginPageView" => new LoginPageView(Services.GetRequiredService<LoginPageViewModel>()),
                    "RegistrationPageView" => new RegistrationPageView(Services.GetRequiredService<RegistrationPageViewModel>()),
                    "AdminPageView" => new AdminPageView(Services.GetRequiredService<AdminPageViewModel>()),
                    "UserPageView" => new UserPageView(Services.GetRequiredService<UserPageViewModel>()),
                    _ => null
                };

                if (page != null)
                    mainWindow.MainFrame.Navigate(page);
            };

            mainWindow.Show();

            await InitializeAsync();

            navigateService.NavigateTo<LoginPageViewModel>();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Ошибка при запуске приложения:\n{ex.Message}\n\nСтек:\n{ex.StackTrace}",
                "Критическая ошибка",
                MessageBoxButton.OK,
                MessageBoxImage.Error);

            Current.Shutdown(-1);
        }
    }
    private async Task InitializeAsync()
    {
        try
        {
            var service = Services.GetRequiredService<Service>();
            var context = Services.GetRequiredService<DataLayer.Models.DataBaseContext>();

            bool imagesExist = context.imagesSets.Any();

            if (!imagesExist)
            {
                await service.AddPicturesToAllTablesAsync();
            }
        }
        catch (Exception ex)
        {
            string fullErrorMessage = $"Ошибка в InitializeAsync:\n{ex.Message}\n\nСтек вызовов:\n{ex.StackTrace}";
            MessageBox.Show(
                fullErrorMessage,
                "КРИТИЧЕСКАЯ ОШИБКА",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}