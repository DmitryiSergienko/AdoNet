using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ViewModel.Core;
public class ObservableObject : INotifyPropertyChanged
{
    public string? Title { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}