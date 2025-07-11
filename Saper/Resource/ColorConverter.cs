using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Saper.Resource
{
    public class ColorConverter : IValueConverter
    {
        public double Strength { get; set; } = 0.5;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color;

            if (value is SolidColorBrush brush)
            {
                color = brush.Color;
            }
            else if (value is Color c)
            {
                color = c;
            }
            else
            {
                return value;
            }

            byte r = (byte)(color.R * Strength);
            byte g = (byte)(color.G * Strength);
            byte b = (byte)(color.B * Strength);

            Color result = Color.FromArgb(color.A, r, g, b);
            return new SolidColorBrush(result);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
