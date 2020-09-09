using System.Windows.Controls;
using WpfApp4.Models;

namespace WpfApp4.Views
{
    /// <summary>
    /// Interaction logic for LeftPanel.xaml
    /// </summary>
    public partial class LeftPanel : UserControl
    {
        DriveModel drive;

        DriveViewModels driveList { get; set; }

        public LeftPanel()
        {
            InitializeComponent();
            driveList = new DriveViewModels();

            drivesListBox.ItemsSource = driveList.Collection;
        }
    }
}
