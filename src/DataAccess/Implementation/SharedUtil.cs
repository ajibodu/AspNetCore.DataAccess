using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;

namespace DataAccess.Implementation
{
    public class SharedUtil
    {
        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            return (from DataRow row in dt.Rows select GetItem<T>(row)).ToList();
        }
        private static T GetItem<T>(DataRow dr)
        {
            var temp = typeof(T);
            var obj = Activator.CreateInstance<T>();
            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (var pro in temp.GetProperties())
                {
                    var displayName = string.Empty;
                    var attribute = pro.GetCustomAttributes(typeof(DisplayNameAttribute), true)
                        .Cast<DisplayNameAttribute>().FirstOrDefault();
                    if (attribute != null)
                        displayName = attribute.DisplayName.ToLower();

                    if (!string.Equals(pro.Name, column.ColumnName, StringComparison.CurrentCultureIgnoreCase) && displayName != column.ColumnName.ToLower()) 
                        continue;
                    
                    if (dr[column.ColumnName] != DBNull.Value)
                    {
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    }
                    else
                    {
                        var typ = dr.GetType();
                        var defValue = GetDefaultValue(typ);
                        pro.SetValue(obj, defValue, null);
                    }
                    break;
                }
            }

            return obj;
        }
        private static object GetDefaultValue(Type t)
        {
            return t.IsValueType ? Activator.CreateInstance(t) : null;
        }
    }
}
