using System.Windows.Input;
using ViewModel.Core;
using ViewModel.Services.Interfaces;

namespace ViewModel.PagesViewModel;
public class RegistrationPageViewModel : BasePageViewModel
{
    public RegistrationPageViewModel() {}
    public ICommand BackToLoginPage { get; }
    public RegistrationPageViewModel(INavigateService navigateService)
    {
        BackToLoginPage = new RelayCommand(obj =>
        {
            navigateService.NavigateTo<LoginPageViewModel>();
        });
    }
}