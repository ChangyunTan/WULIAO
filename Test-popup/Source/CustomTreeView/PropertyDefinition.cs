using System; 
 using System.Collections.Generic; 
 using System.Collections.ObjectModel; 
 using System.Linq; 
 using System.Text; 
 using System.Windows; 
 using System.Windows.Markup;

namespace WpfApplication2.CustomTreeView
{
   public class PropertyObject : DependencyObject
   {
      private static readonly Type _ThisType = typeof(PropertyObject);

      public static readonly DependencyProperty PropertyNameProperty =
          DependencyProperty.Register("PropertyName", typeof(string), _ThisType, new PropertyMetadata(null));

      public string PropertyName
      {
         get
         {
            return (string)GetValue(PropertyNameProperty);
         }
         set
         {
            SetValue(PropertyNameProperty, value);
         }
      }

      public static readonly DependencyProperty PropertyValueProperty =
          DependencyProperty.Register("PropertyValue", typeof(object), _ThisType, new PropertyMetadata(null));

      public object PropertyValue
      {
         get { return GetValue(PropertyValueProperty); }
         set { SetValue(PropertyValueProperty, value); }
      }

      public static readonly DependencyProperty PropertyDescriptionProperty =
          DependencyProperty.Register("PropertyDescription", typeof(string), _ThisType, new PropertyMetadata(null));
      public string PropertyDescription
      {
         get { return (string)GetValue(PropertyDescriptionProperty); }
      }

      public PropertyObject()
      {

      }
   }

   public class ObjectCollection : Collection<PropertyObject>
   {
      public ObjectCollection() { }
   }

   public enum ContentDisplayMode
   {
      Horizontal,
      Vertical
   }

   [ContentProperty("Children")]
   public class PropertyDefinition
   {
      public PropertyDefinition()
      {
         Children = new CollectionProperty();
      }

      public ContentDisplayMode DisplayMode { get; set; }
      public string Caption { get; set; }
      public string Path { get; set; }
      public object HeaderDataTemplate { get; set; }
      public object ContentDataTemplate { get; set; }
      public object TreeViewItemTemplate { get; set; }
      public bool HaveIcon { get; set; }
      public CollectionProperty Children { get; set; }
   }

   public class SessionDefinition : PropertyDefinition
   {
   }

   public class CollectionProperty : Collection<PropertyDefinition>
   {
      public CollectionProperty() { }
   }

   public class PercentageDefinition : PropertyDefinition
   {
      public string PathPercentage { get; set; }
   }

   public class CollectionDefinition : PropertyDefinition
   {
      public Type DataType { get; set; }
      public string ItemSource { get; set; }
   }

   public class InlineTemplate
   {
      public object HeaderDataTemplate { get; set; }
      public object ContentDataTemplate { get; set; }
   }
} 


