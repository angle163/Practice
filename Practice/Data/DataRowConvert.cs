using System;
using System.Data;

namespace Practice.Data
{
    /// <summary>
    /// DataRow转换为基础数据类型的帮助类。
    /// </summary>
    public class DataRowConvert
    {
        #region Constants and Fields

        /// <summary>
        /// DataRown实例。
        /// </summary>
        private readonly DataRow _dbRow;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// 初始化 <see cref="DataRowConvert"/> 类的新实例。
        /// </summary>
        /// <param name="dbRow"> DataRown实例。 </param>
        public DataRowConvert(DataRow dbRow)
        {
            _dbRow = dbRow;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 转换DataRow中指定列名的列为<see cref="bool"/>类型。
        /// </summary>
        /// <param name="columnName"> 转换目标列的列名。 </param>
        /// <returns> 若转换成功返回正确的<see cref="bool"/>值，否则返回<c>nulll</c>. </returns>
        public bool? AsBool(string columnName)
        {
            if (_dbRow[columnName] == DBNull.Value)
            {
                return null;
            }

            return Convert.ToBoolean(_dbRow[columnName]);
        }

        /// <summary>
        /// 转换DataRow中指定列名的列为<see cref="DateTime"/>类型。
        /// </summary>
        /// <param name="columnName"> 转换目标列的列名。 </param>
        /// <returns> 若转换成功返回正确的<see cref="DateTime"/>值，否则返回<c>nulll</c>. </returns>
        public DateTime? AsDateTime(string columnName)
        {
            if (_dbRow[columnName] == DBNull.Value)
            {
                return null;
            }

            return Convert.ToDateTime(_dbRow[columnName]);
        }

        /// <summary>
        /// 转换DataRow中指定列名的列为<see cref="Int32"/>类型。
        /// </summary>
        /// <param name="columnName"> 转换目标列的列名。 </param>
        /// <returns> 若转换成功返回正确的<see cref="Int32"/>值，否则返回<c>nulll</c>. </returns>
        public int? AsInt32(string columnName)
        {
            if (_dbRow[columnName] == DBNull.Value)
            {
                return null;
            }

            return Convert.ToInt32(_dbRow[columnName]);
        }

        /// <summary>
        /// 转换DataRow中指定列名的列为<see cref="Int64"/>类型。
        /// </summary>
        /// <param name="columnName"> 转换目标列的列名。 </param>
        /// <returns> 若转换成功返回正确的<see cref="Int64"/>值，否则返回<c>nulll</c>. </returns>
        public long? AsInt64(string columnName)
        {
            if (_dbRow[columnName] == DBNull.Value)
            {
                return null;
            }

            return Convert.ToInt64(_dbRow[columnName]);
        }

        /// <summary>
        /// 转换DataRow中指定列名的列为<see cref="string"/>类型。
        /// </summary>
        /// <param name="columnName"> 转换目标列的列名。 </param>
        /// <returns> 返回字符串。 </returns>
        public string AsString(string columnName)
        {
            if (_dbRow[columnName] == DBNull.Value)
            {
                return null;
            }

            return _dbRow[columnName].ToString();
        }

        #endregion
    }
}