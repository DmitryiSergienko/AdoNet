using System.Windows.Controls;
using ViewModel.PagesViewModel;

namespace View.Pages;
public partial class RegistrationPageView : Page
{
    public RegistrationPageView(RegistrationPageViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}