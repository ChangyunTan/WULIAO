using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplication2.Data
{
   class StringValueAttribute : Attribute
   {
      public StringValueAttribute(string description)
      {
         Description = description;
      }
      public string Description { get; set; }
   }
}
