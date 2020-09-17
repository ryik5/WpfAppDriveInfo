using System;
using System.Windows;

namespace WpfApp4.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        readonly DriveViewModels driveList = new DriveViewModels();

        public MainView()
        {
            InitializeComponent();


            // Определяем uri на файл с ресурсами стилей
            var uri = new Uri("styles.xaml", UriKind.Relative);
            // загружаем словарь ресурсов
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            // очищаем коллекцию ресурсов приложения
            Application.Current.Resources.Clear();
            // добавляем загруженный словарь ресурсов
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);

            DataContext = driveList;
        }
    }
}