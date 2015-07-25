using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace WpfApplication2.CustomTreeView.Converter
{
   class NullToVisibilityConverter : IValueConverter  
    {
       public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
       {
          if (value != null)
             return System.Windows.Visibility.Visible;
          else
             return System.Windows.Visibility.Collapsed;
       }

       public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
       {
          throw new NotImplementedException();
       } 
    } 

}
