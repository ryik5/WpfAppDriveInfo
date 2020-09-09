using System.Windows;
using WpfApp4.ViewModels;
using System.IO;
using WpfApp4.Models;
using System.Management;
using System;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace WpfApp4.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        CollectDrives drives = new CollectDrives();
           DriveViewModels driveList = new DriveViewModels();

        public MainView()
        {
            InitializeComponent();

            drives.GetDrives(ref driveList);



            DataContext = driveList;

            Task.Run(() => WatchChanges());
        }

        private void watcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                drives.CheckListDrives(ref driveList);
            });
        }

        private void WatchChanges()
        {
            ManagementEventWatcher watcher = new ManagementEventWatcher();
            WqlEventQuery query = new WqlEventQuery("SELECT * FROM Win32_VolumeChangeEvent WHERE EventType = 2");
            watcher.EventArrived += new EventArrivedEventHandler(watcher_EventArrived);
            watcher.Query = query;
            watcher.Start();
            watcher.WaitForNextEvent();
        }

    }
}
