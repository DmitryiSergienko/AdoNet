using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
using ViewModel.Core;
using ViewModel.ModalWindowsViewModel;
using ViewModel.PagesViewModel;
using ViewModel.Services.Interfaces;

namespace ViewModel.Services.Classes;

public class NavigateService : INavigateService
{
    private readonly IServiceProvider _provider;
    private Frame _frame;

    public NavigateService(IServiceProvider provider)
    {
        _provider = provider;
    }

    public event Action<string>? Navigated;

    public void ConfigureNavigation(Frame mainFrame)
    {
        _frame = mainFrame;
    }

    public void NavigateTo<T>() where T : BasePageViewModel
    {
        var viewModel = _provider.GetRequiredService<T>();

        string pageName = viewModel switch
        {
            LoginPageViewModel => "LoginPageView",
            RegistrationPageViewModel => "RegistrationPageView",
            AdminPageViewModel => "AdminPageView",
            UserPageViewModel => "UserPageView",
            RegistrationWindowViewModel => "RegistrationWindowViewModel",
            _ => throw new ArgumentException($"Неизвестная ViewModel: {typeof(T).Name}")
        };

        Navigated?.Invoke(pageName);
    }
}