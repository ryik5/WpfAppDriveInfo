using System.Windows;

namespace WpfApp4.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        DriveViewModels driveList = new DriveViewModels();

        public MainView()
        {
            InitializeComponent();
            
            DataContext = driveList;
        }
    }
}