using System.Globalization;
using System.Windows.Data;

namespace GerenciamentoDeFrota.Helpers
{
    public class BoolToSimNaoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value is true ? "Sim" : "Não";

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value?.ToString() == "Sim";
    }
}