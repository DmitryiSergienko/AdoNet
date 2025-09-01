using System.Windows.Controls;
using ViewModel.Core;

namespace ViewModel.Services.Interfaces;

public interface INavigateService
{
    void NavigateTo<T>() where T : BasePageViewModel;
    void ConfigureNavigation(Frame mainFrame);
    event Action<string> Navigated;
}