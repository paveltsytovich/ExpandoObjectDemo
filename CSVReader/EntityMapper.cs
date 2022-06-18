using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVReader
{
    /// <summary>
    /// Entity mapper class for support mapping from DataTable to Dynamic instance
    /// </summary>
    public static class EntityMapper
    {
        /// <summary>
        /// Create Entity from DataTable row for a specific type
        /// </summary>
        /// <param name="entityType">Entity type object</param>
        /// <param name="table">DataTable instance</param>
        /// <param name="row">DataRow instance</param>
        /// <returns>Instance of dynamic object which created from DataRow instance</returns>
        /// <exception cref="ArgumentException"></exception>
        public static Object MapToSingleEntity(Type entityType, DataTable table, DataRow row)
        {
            if (entityType == null)
                throw new ArgumentException();
            if (table.Rows.Count == 0)
                return null;
            if (typeof(ExpandoObject) == entityType)
                return CreateExpandoObject(table, row);
            else
                return CreateEntity(entityType, table, row);
        }
        /// <summary>
        /// Create Entity for a specific type
        /// </summary>
        /// <param name="entityType">Entity type</param>
        /// <param name="table">DataTable instance</param>
        /// <param name="row">DataRow instance</param>
        /// <returns>Instance of dynamic object which created from DataRow</returns>
        private static object CreateEntity(Type entityType, DataTable table, DataRow row)
        {
            var entity = Activator.CreateInstance(entityType);
            foreach (var column in table.Columns)
            {
                var prop = entity.GetType().GetProperty(column.ToString());
                if(prop != null)
                {
                    if(prop.PropertyType.IsValueType)
                        prop.SetValue(entity,Convert.ToInt64(row[column.ToString()]));  
                    else
                        prop.SetValue(entity, row[column.ToString()]);
                }
            }
            return entity;
        }
        /// <summary>
        /// Create ExpandoObject from DataTable row
        /// </summary>
        /// <param name="table">DataTable instance</param>
        /// <param name="row">DataRow instance</param>
        /// <returns>Instance of Expando object which created from DataRow</returns>
        private static object CreateExpandoObject(DataTable table, DataRow row)
        {
            var entity = new ExpandoObject();
            var dict = entity as IDictionary<string, object>;
            long valLong;
            double valDouble;
            foreach (var column in table.Columns)
            {
                if (Int64.TryParse(row[column.ToString()].ToString(), out valLong))
                    dict.Add(column.ToString(), valLong);                
                else if (Double.TryParse(row[column.ToString()].ToString(), out valDouble))
                {
                    dict.Add(column.ToString(), valDouble);
                }
                else
                   dict.Add(column.ToString(), row[column.ToString()] is DBNull ? "" : row[column.ToString()]);
            }
            return entity;
        }
        /// <summary>
        /// Map from DataTable to Dynamic object
        /// </summary>
        /// <param name="entityType">Entity type</param>
        /// <param name="table">DataTable instance</param>
        /// <returns>List of dynamic objects</returns>
        /// <exception cref="ArgumentException"></exception>
        public static IEnumerable<dynamic> MapTo(Type entityType, DataTable table)
        {
            if (entityType == null) throw new ArgumentException();
            if (table.Rows.Count == 0) return new dynamic[0];
            var entities = new List<dynamic>();
            foreach (DataRow row in table.Rows)
            {
                var entity = MapToSingleEntity(entityType, table, row);
                entities.Add(entity);
            }
            return entities;
        }
    }
}
