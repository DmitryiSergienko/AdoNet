using System.Windows;
using ViewModel.ModalWindowsViewModel;
using ViewModel.Services.Interfaces;

namespace View.ModalWindows
{
    public partial class RegistrationWindowView : Window
    {
        public RegistrationWindowView(RegistrationWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            if (viewModel is IClosable closable)
            {
                closable.RequestClose += () => Close();
            }
        }
        private void TitleBar_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}