using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WpfApplication2.Common;
using WpfApplication2.CustomTreeView.Converter;

namespace WpfApplication2.CustomTreeView
{
   public class PropertyTreeView : TreeView 
   { 
       public PropertyTreeView(): base() 
       { 
           this.Loaded += TreeView_Loaded; 
       } 

       private void TreeView_Loaded(object sender, RoutedEventArgs e) 
       { 
           if (this.PropertyDefinitions != null && this.Items.Count <= 0) 
           { 
               DefinitionTreeViewItems(this.PropertyDefinitions, this); 
           } 
           this.Loaded -= TreeView_Loaded; 
       }

       //protected override DependencyObject GetContainerForItemOverride()
       //{
       //   return new TreeViewItemProperty();
       //}

       //protected override bool IsItemItsOwnContainerOverride(object item)
       //{
       //   return item is TreeViewItemProperty;
       //} 
       private static readonly Type _ThisType = typeof(PropertyTreeView); 

       public static readonly DependencyProperty PropertyDefinitionsProperty = 
         DependencyProperty.Register("PropertyDefinitions", typeof(CollectionProperty), _ThisType, 
         new PropertyMetadata(null)); 

       public CollectionProperty PropertyDefinitions 
       { 
           get 
           { 
               return (CollectionProperty)GetValue(PropertyDefinitionsProperty); 
           } 
           set 
           { 
               SetValue(PropertyDefinitionsProperty, value); 
           } 
       } 

       #region   Definition TreeView Items 

       static PropertyObject NewPropertyObject(string name, string caption, FrameworkElement element) 
       { 
           PropertyObject obj = new PropertyObject() { PropertyName = name }; 

           if (name == null || name == "." || name == "") 
           { 
               obj.SetValue(PropertyObject.PropertyDescriptionProperty, caption); 
               BindingOperations.SetBinding(obj, PropertyObject.PropertyValueProperty, 
                              new Binding() { Source = element, Path = new PropertyPath("DataContext") }); 
           } 
           else 
           { 
               BindingOperations.SetBinding(obj, PropertyObject.PropertyDescriptionProperty, 
                 new Binding() { Source = element, Path = new PropertyPath("DataContext"), ConverterParameter = name, Converter = new PropertyNameToDescriptionConverter() }); 

               BindingOperations.SetBinding(obj, PropertyObject.PropertyValueProperty, 
                               new Binding() { Source = element, Path = new PropertyPath("DataContext." + name) }); 
           } 
           return obj; 
       } 

       static TreeViewItemProperty NewTreeViewItemAndProperty(object obj, out PropertyObject Property) 
       { 
           TreeViewItemProperty item = new TreeViewItemProperty(); 
           item.SetValue(TreeViewItemProperty.DefinitionTypeProperty, obj.GetType().Name); 

           PropertyDefinition definition = (PropertyDefinition)obj; 

           if (definition.HaveIcon) 
               item.SetValue(TreeViewItemProperty.HaveIconProperty, definition.HaveIcon); 
           else 
               item.SetBinding(TreeViewItemProperty.HaveIconProperty, 
                   new Binding("HaveIcon") { RelativeSource = new RelativeSource() { Mode = RelativeSourceMode.FindAncestor, AncestorType = typeof(TreeViewItemProperty) } }); 

           MultiBinding Multbinding = new MultiBinding() { Converter = new ItemsLevelConverter()}; 
           Multbinding.Bindings.Add(new Binding() { Path = new PropertyPath("HaveIcon"), RelativeSource = new RelativeSource() { Mode = RelativeSourceMode.Self } }); 
           Multbinding.Bindings.Add(new Binding() { RelativeSource = new RelativeSource() { Mode = RelativeSourceMode.FindAncestor, AncestorType = typeof(TreeViewItemProperty) } }); 
           item.SetBinding(TreeViewItemProperty.LevelProperty, Multbinding); 

           item.SetValue(TreeViewItemProperty.DisplayModeProperty, definition.DisplayMode); 
           item.SetValue(TreeViewItemProperty.InlineTemplateProperty, 
              new InlineTemplate { HeaderDataTemplate = definition.HeaderDataTemplate, ContentDataTemplate = definition.ContentDataTemplate }); 
           if (definition.TreeViewItemTemplate != null) 
               item.SetValue(TreeViewItemProperty.StyleProperty, definition.TreeViewItemTemplate); 

           Property = NewPropertyObject(definition.Path, definition.Caption, item); 
           item.SetBinding(TreeViewItemProperty.VisibilityProperty, 
               new Binding() { Source = Property, Path = new PropertyPath(CommonUtils.GetPropertyName<PropertyObject>((r) => r.PropertyValue)), Converter = new NullToVisibilityConverter()}); 
           return item; 
        } 
 
        static void AddNewTreeViewItem(PropertyDefinition definition, ItemsControl element) 
        { 
            PropertyObject Property; 
            TreeViewItemProperty item = NewTreeViewItemAndProperty(definition, out Property); 
 
            BindingOperations.SetBinding(item, TreeViewItemProperty.LeftContentProperty, 
                new Binding() { Source = Property, Path = new PropertyPath(CommonUtils.GetPropertyName<PropertyObject>((r) => r.PropertyDescription)) }); 
 
            BindingOperations.SetBinding(item, TreeViewItemProperty.RightContentProperty, 
                new Binding() { Source = Property, Path = new PropertyPath(CommonUtils.GetPropertyName<PropertyObject>((r) => r.PropertyValue)) }); 
            element.Items.Add(item); 
 
            if (definition.Children != null) 
            { 
                DefinitionTreeViewItems(definition.Children, item); 
            } 
        } 
 
        static void AddNewTreeViewItem(PercentageDefinition definition, ItemsControl element) 
        { 
            PropertyObject Property; 
            TreeViewItemProperty item = NewTreeViewItemAndProperty(definition, out Property); 
 
            PropertyObject Percentage = NewPropertyObject(definition.PathPercentage, "", item); 
 
            item.SetValue(TreeViewItemProperty.InlineTemplateProperty, 
               new InlineTemplate { HeaderDataTemplate = definition.HeaderDataTemplate, ContentDataTemplate = definition.ContentDataTemplate }); 
            if (definition.TreeViewItemTemplate != null) 
                item.SetValue(TreeViewItemProperty.StyleProperty, definition.TreeViewItemTemplate); 
 
            BindingOperations.SetBinding(item, TreeViewItemProperty.LeftContentProperty, 
              new Binding() { Source = Property, Path = new PropertyPath(CommonUtils.GetPropertyName<PropertyObject>((r) => r.PropertyDescription)) }); 
            BindingOperations.SetBinding(item, TreeViewItemProperty.RightContentProperty, 
                new Binding() { Source = new ObjectCollection() { Property, Percentage }, Path = new PropertyPath(".") }); 
 
            element.Items.Add(item); 
        } 
 
        static void AddNewTreeViewItem(SessionDefinition definition, ItemsControl element) 
        { 
            TreeViewItemProperty item = new TreeViewItemProperty(); 
            item.SetValue(TreeViewItemProperty.DefinitionTypeProperty, typeof(SessionDefinition).Name); 
 
            if (definition.Caption != null && definition.Caption != "") 
                item.SetValue(TreeViewItemProperty.LeftContentProperty, definition.Caption); 
            item.IsExpanded = true; 
            element.Items.Add(item); 
 
            if (definition.Children != null) 
            { 
                DefinitionTreeViewItems(definition.Children, item); 
                MultiBinding MultiBing = new MultiBinding() { Converter = new ItemsVisibleConverter(), Mode = BindingMode.OneWay }; 
                foreach (TreeViewItem p in item.Items) 
                { 
                    MultiBing.Bindings.Add(new Binding() { Source = p, Path = new PropertyPath("Visibility"), Mode = BindingMode.OneWay }); 
                } 
                item.SetBinding(TreeViewItemProperty.VisibilityProperty, MultiBing); 
            } 
        } 
 
        public static void DefinitionTreeViewItems(CollectionProperty lstDefinition, ItemsControl element) 
        { 
            if (lstDefinition == null) 
                return; 
            for (int i = 0; i < lstDefinition.Count; i++) 
            { 
                if (lstDefinition[i].GetType() == typeof(PropertyDefinition)) 
                { 
                    AddNewTreeViewItem((PropertyDefinition)lstDefinition[i], element); 
                } 
                else if (lstDefinition[i].GetType() == typeof(PercentageDefinition)) 
                { 
                    AddNewTreeViewItem((PercentageDefinition)lstDefinition[i], element); 
                } 
                else if (lstDefinition[i].GetType() == typeof(SessionDefinition)) 
                { 
                    AddNewTreeViewItem((SessionDefinition)lstDefinition[i], element); 
                } 
                else if (lstDefinition[i].GetType() == typeof(CollectionDefinition)) 
                { 
                    AddNewTreeViewItem((CollectionDefinition)lstDefinition[i], element); 
                } 
            } 
        } 
 
        static void AddNewTreeViewItem(CollectionDefinition definition, ItemsControl element) 
        { 
            TreeViewItemProperty item = new TreeViewItemProperty(); 
            item.SetValue(TreeViewItemProperty.DisplayModeProperty, definition.DisplayMode); 
            PropertyObject Property = NewPropertyObject(definition.Path, definition.Caption, item); 
 
            item.SetValue(TreeViewItemProperty.InlineTemplateProperty, 
               new InlineTemplate { HeaderDataTemplate = definition.HeaderDataTemplate, ContentDataTemplate = definition.ContentDataTemplate }); 
            if (definition.TreeViewItemTemplate != null) 
                item.SetValue(TreeViewItemProperty.StyleProperty, definition.TreeViewItemTemplate); 
 
            BindingOperations.SetBinding(item, TreeViewItemProperty.LeftContentProperty, 
                  new Binding() { Source = Property, Path = new PropertyPath(CommonUtils.GetPropertyName<PropertyObject>((r) => r.PropertyDescription)) }); 
            BindingOperations.SetBinding(item, TreeViewItemProperty.RightContentProperty, 
                new Binding() { Source = Property, Path = new PropertyPath(CommonUtils.GetPropertyName<PropertyObject>((r) => r.PropertyValue)) }); 
 
            element.Items.Add(item); 
            if (definition.ItemSource != null && definition.ItemSource != "") 
            { 
                BindingOperations.SetBinding(item, TreeViewItemProperty.ItemsSourceProperty, 
                     new Binding() { Source = element, Path = new PropertyPath("DataContext." + definition.ItemSource) }); 
                if (definition.Children != null) 
                { 
                    PropertyTreeView tree = GetTreeView(item);
                    AddHierarchicalDataTemplate(definition.Children, tree, definition.DataType); 
                } 
            } 
        } 
 
        static PropertyTreeView GetTreeView(FrameworkElement element) 
        { 
            if (element == null) 
                return null; 
            if (element.GetType() == typeof(PropertyTreeView)) 
                return (PropertyTreeView)element; 
            else 
            { 
                return GetTreeView((FrameworkElement)element.Parent); 
            } 
        } 
 
        static void  AddHierarchicalDataTemplate(CollectionProperty lstDefinition, ItemsControl element, object datatype) 
        { 
            HierarchicalDataTemplate template = new HierarchicalDataTemplate(datatype) { VisualTree = new FrameworkElementFactory(typeof(Grid)) }; 
            template.VisualTree.SetValue(Grid.HorizontalAlignmentProperty, HorizontalAlignment.Stretch); 
            DefinitionTreeViewItems(lstDefinition, template.VisualTree, element); 
            element.Resources.Add(template.DataTemplateKey, template); 
        } 
 
        static FrameworkElementFactory NewTreeViewItem(object obj) 
        { 
            PropertyDefinition definition = (PropertyDefinition)obj; 
            FrameworkElementFactory item = new FrameworkElementFactory(typeof(TreeViewItemProperty)); 
            item.SetValue(TreeViewItemProperty.DisplayModeProperty, definition.DisplayMode); 
 
            if (definition.HaveIcon) 
                item.SetValue(TreeViewItemProperty.HaveIconProperty, definition.HaveIcon); 
            else 
                item.SetBinding(TreeViewItemProperty.HaveIconProperty, 
                    new Binding() {
                       Path = new PropertyPath("HaveIcon"),
                       RelativeSource = new RelativeSource() { Mode = RelativeSourceMode.FindAncestor, AncestorType = typeof(TreeViewItemProperty) } }); 
 
            MultiBinding bindingLevel = new MultiBinding() { Converter = new ItemsLevelConverter() }; 
            bindingLevel.Bindings.Add(new Binding() {
               Path = new PropertyPath("HaveIcon"), RelativeSource = new RelativeSource() { Mode = RelativeSourceMode.Self } }); 
            bindingLevel.Bindings.Add(new Binding() { 
               RelativeSource = new RelativeSource() { Mode = RelativeSourceMode.FindAncestor, AncestorType = typeof(TreeViewItemProperty) } }); 
            item.SetBinding(TreeViewItemProperty.LevelProperty, bindingLevel); 
 
            item.SetValue(TreeViewItemProperty.InlineTemplateProperty, 
                   new InlineTemplate { HeaderDataTemplate = definition.HeaderDataTemplate, ContentDataTemplate = definition.ContentDataTemplate }); 
            if (definition.TreeViewItemTemplate != null) 
                item.SetValue(TreeViewItemProperty.StyleProperty, definition.TreeViewItemTemplate); 
 
            return item; 
        } 
        static void AddNewTreeViewItem(PropertyDefinition definition, FrameworkElementFactory elementFactory, ItemsControl tree) 
        { 
            FrameworkElementFactory item = NewTreeViewItem(definition); 
 
            if (definition.Path == "." || definition.Path == null || definition.Path == "") 
            { 
                if (definition.HeaderDataTemplate == null) 
                    item.SetValue(TreeViewItemProperty.LeftContentProperty, definition.Caption); 
                else 
                    item.SetValue(TreeViewItemProperty.LeftContentProperty, new Binding(".")); 
 
                item.SetBinding(TreeViewItemProperty.RightContentProperty, new Binding(".")); 
            } 
            else 
            { 
                MultiBinding multiBinding = new MultiBinding() { Converter = new NameToPropertyObjectConverter(), ConverterParameter = definition.Path }; 
                multiBinding.Bindings.Add(new Binding() { Path = new PropertyPath("DataContext"), RelativeSource = new RelativeSource() { Mode = RelativeSourceMode.Self } }); 
                multiBinding.Bindings.Add(new Binding(definition.Path)); 
                item.SetBinding(TreeViewItemProperty.TagProperty, multiBinding); 
 
                item.SetBinding(TreeViewItemProperty.LeftContentProperty, 
                     new Binding() { Path = new PropertyPath("Tag." + CommonUtils.GetPropertyName<PropertyObject>((r) => r.PropertyDescription)), 
                        RelativeSource = new RelativeSource() { Mode = RelativeSourceMode.Self } }); 
                item.SetBinding(TreeViewItemProperty.RightContentProperty, 
                     new Binding() { Path = new PropertyPath("Tag." + CommonUtils.GetPropertyName<PropertyObject>((r) => r.PropertyValue)), 
                        RelativeSource = new RelativeSource() { Mode = RelativeSourceMode.Self } }); 
            } 
 
            if (definition.Children != null && definition.Children.Count > 0) 
            { 
                DefinitionTreeViewItems((CollectionProperty)definition.Children, item, tree); 
            } 
 
            elementFactory.AppendChild(item); 
        } 
 
        static void AddNewTreeViewItem(CollectionDefinition definition, FrameworkElementFactory elementFactory, ItemsControl tree) 
        { 
            FrameworkElementFactory item = NewTreeViewItem(definition);

            MultiBinding multiBinding = new MultiBinding() { Converter = new NameToPropertyObjectConverter(), ConverterParameter = definition.Path };
            multiBinding.Bindings.Add(new Binding() { Path = new PropertyPath("DataContext"), RelativeSource = new RelativeSource() { Mode = RelativeSourceMode.Self } });
            multiBinding.Bindings.Add(new Binding(definition.Path));
            item.SetBinding(TreeViewItemProperty.TagProperty, multiBinding); 

            item.SetBinding(TreeViewItemProperty.LeftContentProperty, 
                 new Binding() { Path = new PropertyPath("Tag." + CommonUtils.GetPropertyName<PropertyObject>((r) => r.PropertyDescription)),  
                     RelativeSource = new RelativeSource() { Mode = RelativeSourceMode.Self } }); 
            item.SetBinding(TreeViewItemProperty.RightContentProperty, 
                 new Binding() { Path = new PropertyPath("Tag." + CommonUtils.GetPropertyName<PropertyObject>((r) => r.PropertyValue)),  
                     RelativeSource = new RelativeSource() { Mode = RelativeSourceMode.Self } }); 
 
            item.SetBinding(TreeViewItemProperty.ItemsSourceProperty, new Binding(definition.Path)); 
 
            elementFactory.AppendChild(item); 
 
            if (definition.Children != null) 
            { 
                AddHierarchicalDataTemplate(definition.Children, tree, definition.DataType); 
            } 
             
        } 
 
        public static void DefinitionTreeViewItems(CollectionProperty lstDefinition, FrameworkElementFactory elementFactory, ItemsControl tree) 
        { 
            for (int i = 0; i < lstDefinition.Count; i++) 
            { 
                if (lstDefinition[i].GetType() == typeof(PropertyDefinition)) 
                { 
                    AddNewTreeViewItem((PropertyDefinition)lstDefinition[i], elementFactory, tree); 
                } 
                else if (lstDefinition[i].GetType() == typeof(CollectionDefinition)) 
                { 
                    AddNewTreeViewItem((CollectionDefinition)lstDefinition[i], elementFactory, tree); 
                } 
            } 
        } 
        #endregion 
    } 
 
 
} 

