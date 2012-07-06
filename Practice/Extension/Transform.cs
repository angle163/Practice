using System;
using System.Collections.Specialized;
using System.Text;

namespace Practice.Extension
{
    /// <summary>
    /// 对象转换其他类型的方法扩张类。
    /// </summary>
    public static class Transform
    {
        /// <summary>
        /// 将对象转换为<see cref="DateTime"/>类型。
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object obj, DateTime defValue)
        {
            if (obj != null && obj != DBNull.Value)
            {
                return Convert.ToDateTime(obj);
            }

            return defValue;
        }

        /// <summary>
        /// 将对象转换为字符串。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns> 若对象不为<c>null</c>或<see cref="DBNull"/>的值，则返回正确的字符串；否则返回空字符串。 </returns>
        public static string ToStringDBNull(this object obj)
        {
            return ToStringDBNull(obj, string.Empty);
        }

        /// <summary>
        /// 将对象转换为字符串。
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defValue"> 转换失败后，返回默认的字符串。 </param>
        /// <returns> 若对象不为<c>null</c>或<see cref="DBNull"/>的值，则返回正确的字符串；否则返回默认字符串。 </returns>
        public static string ToStringDBNull(this object obj, string defValue)
        {
            return obj != null && obj != DBNull.Value ? obj.ToString() : defValue;
        }

        /// <summary>
        /// 将字符串集合转换为字符串数组。
        /// </summary>
        /// <param name="coll"></param>
        /// <returns></returns>
        public static string[] ToStringArray(this StringCollection coll)
        {
            string[] strRet = new string[coll.Count];
            coll.CopyTo(strRet, 0);
            return strRet;
        }

        /// <summary>
        /// 将对象转换为布尔值。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool ToBool(this object obj)
        {
            return ToBool(obj, false);
        }

        /// <summary>
        /// 将对象转换为布尔值。
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static bool ToBool(this object obj, bool defValue)
        {
            bool value;
            if (obj != null && obj != DBNull.Value && bool.TryParse(obj.ToString(), out value))
            {
                return value;
            }

            return defValue;
        }

        /// <summary>
        /// 将对象转化为<see cref="int"/>，转换失败返回<c>0</c>.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToInt(this object obj)
        {
            return obj != null && obj != DBNull.Value ? Convert.ToInt32(obj) : 0;
        }

        /// <summary>
        /// 将<see cref="byte"/>数组转换为16进制的字符串。
        /// </summary>
        /// <param name="hashedBytes"></param>
        /// <returns></returns>
        public static string ToHexString(this byte[] hashedBytes)
        {
            if (hashedBytes == null || hashedBytes.Length == 0)
            {
                throw new ArgumentException("hasdedBytes is null or empty.", "hashedBytes");
            }

            var hashedSB = new StringBuilder((hashedBytes.Length * 2) + 2);
            foreach (byte b in hashedBytes)
            {
                hashedSB.AppendFormat("{0:X2}", b);
            }

            return hashedSB.ToString();
        }
    }
}