using System.Windows;

namespace View;
/// <summary>
/// Логика взаимодействия для ContainerWindow.xaml
/// </summary>
public partial class ContainerWindow : Window
{
    public ContainerWindow()
    {
        InitializeComponent();
    }
    private void TitleBar_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            DragMove();
    }
    private void MinimizeButton_Click(object sender, RoutedEventArgs e)
    {
        this.WindowState = WindowState.Minimized;
    }
    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}