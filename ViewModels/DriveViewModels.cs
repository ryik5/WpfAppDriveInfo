using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp4.Models;

namespace WpfApp4.ViewModels
{
  public  class DriveViewModels: INotifyPropertyChanged
    {
        public ObservableCollection<DriveModel> Collection { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
