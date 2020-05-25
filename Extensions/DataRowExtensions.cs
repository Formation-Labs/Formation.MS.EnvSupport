using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Formation.MS.EnvSupport.Extensions
{
    public static class DataRowExtensions
    {
        public static T ToObject<T>(this DataRow dataRow)
        where T : new()
        {
            if (dataRow is null)
            {
                throw new System.ArgumentNullException(nameof(dataRow));
            }

            T item = new T();

            foreach(DataColumn column in dataRow.Table.Columns)
            {
                PropertyInfo property = GetProperty(typeof(T), column.ColumnName);

                if (property != null && dataRow[column] != DBNull.Value && dataRow[column].ToString() != "NULL")
                {
                   property.SetValue(item, ChangeType(dataRow[column], property.PropertyType), null); 
                }
            }

            return item;
        }

        private static PropertyInfo GetProperty(Type type, string attributeName)
        {
            PropertyInfo property = type.GetProperty(attributeName);
            return property ?? Array.Find(type.GetProperties(), p => p.IsDefined(typeof(DisplayAttribute), false)
                            && p.GetCustomAttributes(typeof(DisplayAttribute), false)
                                .Cast<DisplayAttribute>().Single().Name == attributeName);
        }

        public static object ChangeType(object value, Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }
                return Convert.ChangeType(value, Nullable.GetUnderlyingType(type));
            }
            return Convert.ChangeType(value, type);
        }


    }
}