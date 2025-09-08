using System.ComponentModel;
using System.Windows;
using ViewModel.ModalWindowsViewModel;
using ViewModel.Services.Interfaces;

namespace View.ModalWindows
{ 
    public partial class DeleteHumanWindowView : Window
    {
        private DeleteHumanWindowViewModel _viewModel;
        public DeleteHumanWindowView(DeleteHumanWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            _viewModel = viewModel;

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
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (DataContext is DeleteHumanWindowViewModel vm)
            {
                vm.ClearData();
            }
        }
    }
}
