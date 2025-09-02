using System.Windows;
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
    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        var viewModel = DataContext as RegistrationPageViewModel;
        if (viewModel != null)
        {
            viewModel.Password = ((PasswordBox)sender).Password;
        }
    }
}