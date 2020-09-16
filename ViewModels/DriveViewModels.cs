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
    public class DriveViewModels : INotifyPropertyChanged, IDisposable
    {
        private readonly ManagementEventWatcher watcher;
        private DriveModel selectedDrive;
        private readonly CollectDrives drives = new CollectDrives();

        public ObservableCollection<DriveModel> Collection { get; set; }

        public DriveModel SelectedDrive
        {
            get { return selectedDrive; }
            set
            {
                selectedDrive = value;
                OnPropertyChanged("SelectedDrive");
            }
        }

        public DriveViewModels()
        {
            Collection = new ObservableCollection<DriveModel>();

            foreach (var drive in drives.GetDrives())
            {
                Collection.Add(drive);
            }

            watcher = new ManagementEventWatcher();
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).

                    watcher?.Stop();
                    watcher?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~DriveViewModels()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}