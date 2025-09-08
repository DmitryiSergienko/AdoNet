namespace ViewModel.Services.Interfaces
{
    public interface IClosable
    {
        event Action? RequestClose;
    }
}