using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace WpfApplication2.CustomTreeView.Converter
{
  public class NameToPropertyObjectConverter : IMultiValueConverter 
    { 
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) 
        { 
            if (values == null) 
                return null; 
 
            PropertyObject obj = new PropertyObject { PropertyName = parameter.ToString() }; 
            obj.SetValue(PropertyObject.PropertyDescriptionProperty, (string)(new PropertyNameToDescriptionConverter().Convert(values[0], null, obj.PropertyName, null))); 
            obj.SetValue(PropertyObject.PropertyValueProperty, values[1]); 
            return obj; 
        } 
 
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) 
        { 
            throw new NotImplementedException(); 
        } 
    } 

}
