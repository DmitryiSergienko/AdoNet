using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using ViewModel.PagesViewModel;
using View.Helpers;

namespace View.Pages;
public partial class AdminPageView : Page
{
    public AdminPageView(AdminPageViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
    private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
    {
        // Проверяем ТОЛЬКО по имени столбца. Это надежнее.
        if (e.PropertyName == "Image")
        {
            // Создаём шаблонный столбец
            var templateColumn = new DataGridTemplateColumn
            {
                Header = "image", // Можно задать свой заголовок
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
                Converter = new ByteToImageConverter() // Используем ваш существующий конвертер
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