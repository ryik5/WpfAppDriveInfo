using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WpfApp4.Models;

namespace WpfApp4
{
    public class DriveViewModels : INotifyPropertyChanged
    {
        private DriveModel selectedDrive;
        CollectDrives drives = new CollectDrives();

        public ObservableCollection<DriveModel> Collection { get; set; }

        public DriveModel SelectedDrive
        {
            get { return selectedDrive; }
            set
            {
                selectedDrive = value;
                OnPropertyChanged("SelectedDrive");
                OnPropertyChanged("MyTitle");
            }
        }

        public DriveViewModels()
        {
            Collection = new ObservableCollection<DriveModel>();

            foreach (var drive in drives.GetDrives())
            {
                Collection.Add(drive);
            }

            ManagementEventWatcher watcher = new ManagementEventWatcher();
            Task.Run(() => WatchChanges(watcher));
        }

        private ManagementEventWatcher WatchChanges(ManagementEventWatcher watcher)
        {
            WqlEventQuery query = new WqlEventQuery("SELECT * FROM Win32_VolumeChangeEvent WHERE EventType = 2 OR EventType = 3");
            watcher.EventArrived += new EventArrivedEventHandler(watcher_EventArrived);
            watcher.Query = query;
            watcher.Start();
            watcher.WaitForNextEvent();
            return watcher;
        }

        private void watcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            IList<DriveModel> currentList = drives.GetDrives();
            IList<DriveModel> previousList = Collection.ToList();

            App.Current.Dispatcher.Invoke((Action)delegate
                {
                    if (currentList.Count > previousList.Count)
                    {
                        foreach (var drive in currentList)
                        {
                            if (!Collection.Any(p => p.ToString().Equals(drive.ToString())))
                            {
                                Collection.Add(drive);
                            }
                        }
                    }
                    else
                    {
                        foreach (var drive in previousList)
                        {
                            if (!currentList.Any(p => p.ToString().Equals(drive.ToString())))
                            {
                                Collection.Remove(drive);
                            }
                        }
                    }
                });
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}