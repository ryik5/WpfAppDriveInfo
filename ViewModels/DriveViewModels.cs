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
            }
        }


        public DriveViewModels()
        {
            Collection = new ObservableCollection<DriveModel>();

            foreach (var drive in drives.GetDrives())
            {
                Collection.Add(drive);
            }

            Task.Run(() => WatchChanges());
        }



        private void watcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            IList<DriveModel> currentList = drives.GetDrives();
            var names=new HashSet<string>(currentList.Select(x => x.GetId()));

            App.Current.Dispatcher.Invoke((Action)delegate
                {
                    if (currentList.Count > Collection.Count)
                    {
                        foreach (var driveCurrentList in currentList)
                        {
                            if (!Collection.Any(p => p.GetId().Equals(driveCurrentList.GetId())))
                            {
                                Collection.Add(driveCurrentList);
                            }
                        }
                    }
                    else
                    {
                        //foreach( var book in e.OldItems.OfType<Book>().Select( v => new BookViewModel( v ) ) ) {
                        //   this.Remove(book);                    }

                        //foreach (var book in e.OldItems.OfType<Book>())
                        //{
                        //    BookViewModel tempBVM = this.Where(x => x.Book.name == book.name &&
                        //       c.Book.author == book.author).FirstOrDefault();
                        //    if (tempBVM != null)
                        //    {
                        //        this.Remove(tempBVM);
                        //    }
                        //}

                        Collection.Remove(x => !names.Contains(x.GetId()));
                    }

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






        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
