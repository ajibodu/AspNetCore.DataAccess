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
        public List<T> convertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    T item = GetItem<T>(row);
                    data.Add(item);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();
            try
            {
                foreach (DataColumn column in dr.Table.Columns)
                {
                    foreach (PropertyInfo pro in temp.GetProperties())
                    {
                        string displayName = string.Empty;
                        var attribute = pro.GetCustomAttributes(typeof(DisplayNameAttribute), true)
                                                .Cast<DisplayNameAttribute>().FirstOrDefault();
                        if (attribute != null)
                            displayName = attribute.DisplayName.ToLower();

                        if (pro.Name.ToLower() == column.ColumnName.ToLower() || displayName == column.ColumnName.ToLower())
                        {
                            if (dr[column.ColumnName] != DBNull.Value)
                            {
                                pro.SetValue(obj, dr[column.ColumnName], null);
                            }
                            else
                            {
                                Type typ = dr.GetType();
                                var defValue = GetDefaultValue(typ);
                                pro.SetValue(obj, defValue, null);
                            }
                            break;
                        }
                        else
                            continue;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return obj;
        }
        private static object GetDefaultValue(Type t)
        {
            if (t.IsValueType)
                return Activator.CreateInstance(t);

            return null;
        }
    }
}
