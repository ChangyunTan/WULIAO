using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using WpfApplication2.Data;

namespace WpfApplication2.Converter
{
   public class ColumnGroupToStringConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
      {
         if (value == null)
            return null;
         List<ColumnTypeDto> columnList = new List<ColumnTypeDto>();
         foreach (var group in (List<ColumnGroupTypeDto>)value)
         {
            columnList.AddRange(group.ColumnList);
         }
         var names = columnList.GroupBy(r => r.Name).Select(g => g.Key);
         return string.Join(",", names.ToArray());
      }

      public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
      {
         throw new NotImplementedException();
      }
   }
}
