using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace StopwatchApplication.Services.Converters
{
    /// <summary>
    /// Класс, выполняющий конвертирование кол-ва кругов больших ила равных 1 в значение True, иначе False.
    /// </summary>
    class LapIsMoreThanOneConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value >= 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
