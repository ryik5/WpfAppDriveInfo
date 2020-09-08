using System.Windows;
using WpfApp4.ViewModels;
using WpfApp4.Models;

namespace WpfApp4.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public DriveViewModels driveList;

        public MainView()
        {
            InitializeComponent();

            driveList = new DriveViewModels();

            DriveModel drive = new DriveModel { Name = "c:\\", Type = DiskType.Fixed , FileSystem= FileSystem.NTFS};
            driveList.Collection.Add(drive);

            DataContext = driveList;
        }
    }
}
