using System.ComponentModel;
using System.Windows;

namespace WpfApp4.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        DriveViewModels driveList = new DriveViewModels();

        public MainView()
        {
            InitializeComponent();
            
            DataContext = driveList;
            MyTitle =  "WPF";
        }


        public string MyTitle
        {
            get { return (string)GetValue(MyTitleProperty); }
            set { SetValue(MyTitleProperty, value); }
        }

        public static readonly DependencyProperty MyTitleProperty =
            DependencyProperty.Register("MyTitle", typeof(string), typeof(MainView), new UIPropertyMetadata(null));

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }    
}