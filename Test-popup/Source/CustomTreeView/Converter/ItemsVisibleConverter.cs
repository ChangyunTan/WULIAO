using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace WpfApplication2.CustomTreeView.Converter
{
  public class ItemsVisibleConverter : IMultiValueConverter 
     { 
         public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) 
         { 
             if (values == null) 
                 return System.Windows.Visibility.Collapsed; 
  
             System.Windows.Visibility visibility = System.Windows.Visibility.Collapsed; 
             foreach (var p in values) 
             { 
                 if ((System.Windows.Visibility)p == System.Windows.Visibility.Visible) 
                 { 
                     visibility = System.Windows.Visibility.Visible; 
                     break; 
                 } 
  
             } 
             return visibility; 
         } 
  
         public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) 
         { 
             throw new NotImplementedException(); 
         } 
     } 

}
