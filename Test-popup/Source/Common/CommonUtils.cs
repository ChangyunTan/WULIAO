using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace WpfApplication2.Common
{
   public static class CommonUtils
   {
      private static readonly string C_SEPARATOR = ".";
      /// <summary>
      /// Get the proeprty name from a lambda expression.
      /// </summary>
      [System.Diagnostics.DebuggerHidden]
      private static string GetPropertyName(Expression<Func<object>> aCallProperty)
      {
         return ExtractPropertyPath((LambdaExpression)aCallProperty);
      }

      [System.Diagnostics.DebuggerHidden]
      public static string GetPropertyName<TClass>(Expression<Func<TClass, object>> aExpression)
      {
         return ExtractPropertyPath(aExpression);
      }

      public static string GetPropertyDescription(Type TClass, string PropertyName)
      {
         PropertyInfo perperty = TClass.GetProperty(PropertyName);
         object[] att = perperty.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
         if (att.Length > 0)
            return ((System.ComponentModel.DescriptionAttribute)(att[0])).Description;
         else
            return PropertyName;
      }

      public static string GetPropertyDescription<TClass>(Expression<Func<TClass, object>> aExpression)
      {
         MemberExpression memberExpression = GetMemberExpression(aExpression);

         object[] att = memberExpression.Member.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
         if (att.Length > 0)
            return ((System.ComponentModel.DescriptionAttribute)(att[0])).Description;
         else
            return memberExpression.Member.Name;
      }

      private static MemberExpression GetMemberExpression(System.Linq.Expressions.Expression aExpression)
      {
         if (aExpression is MemberExpression)
         {
            return (MemberExpression)aExpression;
         }
         else if (aExpression is LambdaExpression)
         {
            var lambdaExpression = aExpression as LambdaExpression;
            if (lambdaExpression.Body is MemberExpression)
            {
               return (MemberExpression)lambdaExpression.Body;
            }
            else if (lambdaExpression.Body is UnaryExpression)
            {
               return ((MemberExpression)((UnaryExpression)lambdaExpression.Body).Operand);
            }
         }
         return null;
      }

      private static string ExtractPropertyPath(System.Linq.Expressions.Expression aExpression)
      {
         var path = new StringBuilder();
         MemberExpression memberExpression = GetMemberExpression(aExpression);
         do
         {
            if (path.Length > 0)
            {
               path.Insert(0, C_SEPARATOR);
            }
            path.Insert(0, memberExpression.Member.Name);
            memberExpression = GetMemberExpression(memberExpression.Expression);
         }
         while (memberExpression != null);
         return path.ToString();
      }

      public static T GetPropertyValue<T>(object aOwnerObject, string aPropertyName)
      {
         if (aOwnerObject == null)
            throw new ArgumentNullException("aOwnerObject can not be null.");

         if (string.IsNullOrEmpty(aPropertyName))
            throw new ArgumentNullException("aPropertyName can not be null.");

         int index = aPropertyName.IndexOf(C_SEPARATOR);
         if (index == -1)
            return GetSinglePropertyValue<T>(aOwnerObject, aPropertyName);
         else
         {
            object obj = GetSinglePropertyValue<object>(aOwnerObject, aPropertyName.Substring(0, index));
            return CommonUtils.GetPropertyValue<T>(obj, aPropertyName.Substring(index + 1));
         }
      }

      private static T GetSinglePropertyValue<T>(object aOwnerObject, string aPropertyName)
      {
         PropertyInfo perperty = aOwnerObject.GetType().GetProperty(aPropertyName);
         if (perperty == null)
            throw new ArgumentException("perperty can not be null.");
         else
            return (T)perperty.GetValue(aOwnerObject, null);
      }
   }
}
