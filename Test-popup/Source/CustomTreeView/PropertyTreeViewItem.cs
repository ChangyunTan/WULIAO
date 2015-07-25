using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WpfApplication2.CustomTreeView
{
   public class TreeViewItemProperty : TreeViewItem 
   { 
       public TreeViewItemProperty() 
       { 

       }
       //protected override DependencyObject GetContainerForItemOverride()
       //{
       //   return new TreeViewItemProperty();
       //}

       //protected override bool IsItemItsOwnContainerOverride(object item)
       //{
       //   if (item.GetType() == typeof(TreeViewItem))
       //      return item is TreeViewItemProperty;
       //   else
       //      return item is TreeViewItemProperty;
       //} 

       private static readonly Type _ThisType = typeof(TreeViewItemProperty); 

       public static readonly DependencyProperty LeftContentProperty = 
          DependencyProperty.Register("LeftContent", typeof(object), _ThisType, new PropertyMetadata(null)); 

       public object LeftContent 
       { 
           get 
           { 
               return (object)GetValue(LeftContentProperty); 
           } 
           set 
           { 
               SetValue(LeftContentProperty, value); 
           } 
       } 

       public static readonly DependencyProperty RightContentProperty = 
           DependencyProperty.Register("RightContent", typeof(object), _ThisType, new PropertyMetadata(null)); 

       public object RightContent 
       { 
           get 
           { 
               return (object)GetValue(RightContentProperty); 
           } 
           set 
           { 
               SetValue(RightContentProperty, value); 
           } 
       } 

       public static readonly DependencyProperty InlineTemplateProperty = 
           DependencyProperty.Register("InlineTemplate", typeof(InlineTemplate), _ThisType, new PropertyMetadata(null)); 

       public InlineTemplate InlineTemplate 
       { 
           get 
           { 
               return (InlineTemplate)GetValue(InlineTemplateProperty); 
           } 
           set 
           { 
               SetValue(InlineTemplateProperty, value); 
           } 
       } 

       public static readonly DependencyProperty DisplayModeProperty = 
           DependencyProperty.Register("DisplayMode", typeof(ContentDisplayMode), _ThisType, new PropertyMetadata(ContentDisplayMode.Horizontal)); 

       public ContentDisplayMode DisplayMode 
       { 
           get 
           { 
               return (ContentDisplayMode)GetValue(DisplayModeProperty); 
           } 
           set 
           { 
               SetValue(DisplayModeProperty, value); 
           } 
       } 

       public static readonly DependencyProperty DefinitionTypeProperty = 
           DependencyProperty.Register("DefinitionType", typeof(string), _ThisType, new PropertyMetadata(null)); 

       public string DefinitionType 
       { 
           get 
           { 
               return (string)GetValue(DefinitionTypeProperty); 
           } 
           set 
           { 
                SetValue(DefinitionTypeProperty, value); 
            } 
        } 
 
        public static readonly DependencyProperty HaveIconProperty = 
         DependencyProperty.Register("HaveIcon", typeof(bool), _ThisType, new PropertyMetadata(false)); 
 
        public bool HaveIcon 
        { 
            get 
            { 
                return (bool)GetValue(HaveIconProperty); 
            } 
            set 
            { 
                SetValue(HaveIconProperty, value); 
            } 
        } 
 
        public static readonly DependencyProperty LevelProperty = 
         DependencyProperty.Register("Level", typeof(int), _ThisType, new PropertyMetadata(0)); 
 
        public int Level 
        { 
            get 
            { 
                return (int)GetValue(LevelProperty); 
            } 
            set 
            { 
                SetValue(LevelProperty, value); 
            } 
        } 
    } 
} 
