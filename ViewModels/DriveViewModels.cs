using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfApp4.Models;

namespace WpfApp4.ViewModels
{
    public class DriveViewModels : INotifyPropertyChanged
    {
        private DriveModel selectedDrive;
        public ObservableCollection<DriveModel> Collection { get; set; }
        public List<string> GetUniqDrives()
        {
            List<string> list = new List<string>();

            if (Collection?.Count > 0)
            {
                foreach (var d in Collection)
                {
                    list.Add(d.GetId());
                }
            }
            return list;
        }
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
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
