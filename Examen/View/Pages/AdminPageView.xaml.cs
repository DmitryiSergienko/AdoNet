using System.Windows.Controls;
using ViewModel.PagesViewModel;

namespace View.Pages;
public partial class AdminPageView : Page
{
    public AdminPageView(AdminPageViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}