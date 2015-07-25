using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace WpfApplication2.CustomTreeView.Converter
{
   public class ItemsLevelConverter: IMultiValueConverter 
      { 
         public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) 
         { 
             if (values == null || (!(bool)values[0]) || values[1] == null) 
                 return 0; 
              
             TreeViewItemProperty item = (TreeViewItemProperty)values[1];
             if (!item.HaveIcon)
                return 0;
             else
             {
                if (item.Template.FindName("PART_Header", item) == null)
                   return item.Level;
                else
                   return item.Level + 1;
             }
                  
         } 
  
         public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) 
         { 
             throw new NotImplementedException(); 
         } 
     }

}
