using System.Windows.Controls;
using ViewModel.PagesViewModel;

namespace View.Pages;
public partial class UserPageView : Page
{
    public UserPageView(UserPageViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}