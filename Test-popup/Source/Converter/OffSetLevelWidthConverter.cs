using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using WpfApplication2.CustomTreeView;

namespace WpfApplication2.Converter
{
  public class OffSetLevelWidthConverter : IMultiValueConverter 
     { 
         public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) 
         { 
             if (values == null) 
                 return null; 
             TreeViewItemProperty p = (TreeViewItemProperty)values[0]; 
             if (!p.HaveIcon) 
                 return new GridLength(0); 
             else 
             {
                double offsetWidth;
                if (!double.TryParse(values[1].ToString(), out offsetWidth))
                   offsetWidth = 16.0;
                double offset = p.Level * offsetWidth; 
                return new GridLength(offset); 
             } 
         } 
  
         public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) 
         { 
             throw new NotImplementedException(); 
         } 
     } 
}
