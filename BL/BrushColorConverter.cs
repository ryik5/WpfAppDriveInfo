using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using WpfApp4.Models;

namespace WpfApp4.BL
{
    public class BrushColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = Colors.Black;

            if (value is null)
                return new SolidColorBrush(result);

            var diskType = DiskType.Unknown;
            try { diskType = (DiskType)value; } catch { }

            switch (diskType)
            {
                case DiskType.Removable:
                    result = Colors.PaleGreen;
                    break;
                case DiskType.Fixed:
                    result = Colors.Black;
                    break;
                default:
                    result = Colors.PaleVioletRed;
                    break;
            }

            return new SolidColorBrush(result);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
