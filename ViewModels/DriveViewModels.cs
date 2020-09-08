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
            Collection = new ObservableCollection<DriveModel>(); }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private RelayCommand addDrive;
        public RelayCommand AddDrive
        {
            get
            {
                return addDrive ??
                  (addDrive = new RelayCommand(obj =>
                  {
                      DriveModel drive = new DriveModel() { Name = "new" };
                      Collection.Add(drive);
                      SelectedDrive = drive;

                  }));
            }
        }

    }
}
