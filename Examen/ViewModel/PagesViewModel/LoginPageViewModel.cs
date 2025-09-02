using System.Windows.Input;
using ViewModel.Core;
using ViewModel.Services.Interfaces;

namespace ViewModel.PagesViewModel;
public class LoginPageViewModel : BasePageViewModel
{
    public LoginPageViewModel() { } // Нужен для дизайнера
    public ICommand NavigateToSignIn { get; }
    public ICommand NavigateToRegistration { get; }
    public LoginPageViewModel(INavigateService navigateService)
    {
        NavigateToSignIn = new RelayCommand(obj => navigateService.NavigateTo<AdminPageViewModel>());
        NavigateToRegistration = new RelayCommand(obj => navigateService.NavigateTo<RegistrationPageViewModel>());
    }
}