using DataLayer.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using View.Pages;
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
        string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=TZ_21_07_2025;Trusted_Connection=true;TrustServerCertificate=true;";
        services.AddDbContext<DataLayer.Models.TZ_21_07_2025Context>(options =>
        options.UseSqlServer(connectionString));

        services.AddDbContext<DataLayer.Procedures.TZ_21_07_2025Context>(options =>
            options.UseSqlServer(connectionString));

        services.AddDbContext<DataLayer.Views.TZ_21_07_2025Context>(options =>
            options.UseSqlServer(connectionString));
        services.AddScoped<UserService>();
        services.AddSingleton<INavigateService, NavigateService>();
        services.AddTransient<LoginPageViewModel>();
        services.AddTransient<RegistrationPageViewModel>();
        services.AddTransient<AdminPageViewModel>();
        services.AddTransient<UserPageViewModel>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var mainWindow = new ContainerWindow();
        var navigateService = Services.GetService<INavigateService>();

        // Настраиваем Frame
        navigateService?.ConfigureNavigation(mainWindow.MainFrame);

        // 🔽 ПОДПИСКА: создаём Page в зависимости от ViewModel
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

            if (page != null && navigateService != null)
                navigateService.ConfigureNavigation(mainWindow.MainFrame); // убедимся, что Frame актуален
            mainWindow.MainFrame.Navigate(page);
        };

        // 🔽 Первый переход
        navigateService?.NavigateTo<LoginPageViewModel>();

        mainWindow.Show();
    }
}