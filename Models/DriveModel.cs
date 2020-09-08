using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp4.Models
{
    public class DriveModel:INotifyPropertyChanged
    {
        private string name;
        private DiskType type;
        private FileSystem fileSystem;
        private int totalSpace;
        private int freeSpace;

        public string Name {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public DiskType Type {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged("Type");
            }
        }
        public FileSystem FileSystem {
            get { return fileSystem; }
            set
            {
                fileSystem = value;
                OnPropertyChanged("FileSystem");
            }
        }
        public int TotalSpace {
            get { return totalSpace; }
            set
            {
                totalSpace = value;
                OnPropertyChanged("TotalSpace");
            }
        }
        public int FreeSpace {
            get { return freeSpace; }
            set
            {
                freeSpace = value;
                OnPropertyChanged("FreeSpace");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
