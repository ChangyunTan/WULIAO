using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using WpfApplication2.Common;

namespace WpfApplication2.CustomTreeView.Converter
{

    public class PropertyNameToDescriptionConverter : IValueConverter 
    { 
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) 
        { 
            if (value == null) 
                return null; 
            try 
            { 
                return CommonUtils.GetPropertyDescription(value.GetType(), parameter.ToString()); 
            } 
            catch 
            { 
                return ""; 
            } 
        } 
 
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) 
        { 
            throw new NotImplementedException(); 
        } 
    } 
} 
