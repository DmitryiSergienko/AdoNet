using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using View.Helpers;
using ViewModel.PagesViewModel;

namespace View.Pages;
public partial class UserPageView : Page
{
    public UserPageView(UserPageViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
    private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
    {
        // Проверяем, если столбец называется "Image" и содержит byte[]
        if (e.PropertyName == "Image" && e.PropertyType == typeof(byte[]))
        {
            // Создаём шаблонный столбец
            var templateColumn = new DataGridTemplateColumn
            {
                Header = e.Column.Header,
                SortMemberPath = e.PropertyName
            };

            // Создаём DataTemplate с Image
            var factory = new FrameworkElementFactory(typeof(Image));
            factory.SetValue(Image.StretchProperty, Stretch.Uniform);
            factory.SetValue(Image.WidthProperty, 50.0);
            factory.SetValue(Image.HeightProperty, 50.0);

            // Привязка с конвертером
            var binding = new Binding(e.PropertyName)
            {
                Converter = new ByteToImageConverter()
            };
            factory.SetBinding(Image.SourceProperty, binding);

            var dataTemplate = new DataTemplate();
            dataTemplate.VisualTree = factory;
            templateColumn.CellTemplate = dataTemplate;

            // Заменяем стандартный столбец на шаблонный
            e.Column = templateColumn;
        }
    }
}