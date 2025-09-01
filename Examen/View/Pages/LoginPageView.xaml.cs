using System.Windows.Controls;
using ViewModel.PagesViewModel;

namespace View.Pages;
public partial class LoginPageView : Page
{
    public LoginPageView(LoginPageViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}