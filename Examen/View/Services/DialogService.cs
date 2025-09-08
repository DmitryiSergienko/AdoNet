using System.Windows;
using View.ModalWindows;
using ViewModel.ModalWindowsViewModel;

public class DialogService : IDialogService
{
    public void ShowModal(object viewModel)
    {
        Window window = viewModel switch
        {
            RegistrationWindowViewModel regVm => new RegistrationWindowView(regVm),
            DeleteHumanWindowViewModel delVm => new DeleteHumanWindowView(delVm),
            _ => throw new ArgumentException($"Неизвестная ViewModel: {viewModel?.GetType().Name}")
        };

        window.Owner = Application.Current.MainWindow;
        window.ShowDialog();
    }
}