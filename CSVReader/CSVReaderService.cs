using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using LumenWorks.Framework.IO.Csv;
using System.Collections;
using System.Dynamic;

namespace CSVReader
{
    /// <summary>
    /// CSV Reader service
    /// </summary>
    public class CSVReaderService : IEnumerable<Object>
    {
        private DataTable _table;
        private readonly string _fileName;
        private readonly char _separator;
        public CSVReaderService(string fileName,char separator = ',')
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException();
            if (!File.Exists(fileName))
                throw new DataFileReadErrorException($"File {fileName} doesn't exists");
            _fileName = fileName;
            _separator = separator;
        }
        /// <summary>
        /// Read CSV file and load into DataTable
        /// </summary>
        private void ReadCsv()
        {
            if(_table == null)
            {
                using(var csvRead = new CsvReader(new StreamReader(_fileName),true, _separator))
                {
                    _table = new DataTable();
                    _table.Load(csvRead);
                    FixColumsName();
                }
            }
        }
        /// <summary>
        /// Get Iterator instance
        /// </summary>
        /// <returns></returns>
        public IEnumerator<dynamic> GetEnumerator()
        {
            ReadCsv();           
            return EntityMapper.MapTo(typeof(ExpandoObject), _table).GetEnumerator();
        }
        /// <summary>
        /// Fix column name with C# syntax
        /// </summary>
        private void FixColumsName()
        {
            foreach(DataColumn c in _table.Columns)
            {
                c.ColumnName = c.ColumnName.Replace(' ', '_').Replace('/', '_');
            }
        }
        /// <summary>
        /// Get Iterator instance
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            ReadCsv();
            return EntityMapper.MapTo(typeof(ExpandoObject), _table).GetEnumerator();
        }
    }
}
