using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using Practice.Types;
using Practice.Types.Annotation;

namespace Practice.Extension
{
    /// <summary>
    /// The db helper extension.
    /// </summary>
    public static class DBHelperExtension
    {
        /// <summary>
        /// The to trace string.
        /// </summary>
        /// <param name="command">
        /// The command.
        /// </param>
        /// <returns>
        /// Returns the Debug String
        /// </returns>
        public static string ToDebugString([NotNull] this IDbCommand command)
        {
            CodeContract.ArgumentNotNull(command, "command");

            string debugString = command.CommandText;

            try
            {
                if (command.Parameters != null)
                {
                    debugString = command.Parameters.Cast<DbParameter>().Aggregate(
                        debugString,
                        (current, p) =>
                        "{0}{1}".FormatWith(current, ("\n[" + p.ParameterName + "] - [" + p.DbType + "] - [" + p.Value + "]")));
                }
            }
            catch (Exception ex)
            {
                debugString += "Error in getting parameters {0}".FormatWith(ex);
            }

            return debugString;
        }

        /// <summary>
        /// 将当前实例的类型转换为<see cref="T"/>类型。
        ///     若实例为空或者实例与<see cref="T"/>类型不兼容则返回<c>null</c>,
        ///     否则，返回<see cref="T"/>类型的实例。
        /// </summary>
        /// <typeparam name="T"> 转换的目标类型。 </typeparam>
        /// <param name="instance"> 当前实例。 </param>
        /// <returns> 转换成功返回<see cref="T"/>类型实例，否则返回<c>null</c>. </returns>
        [CanBeNull]
        public static T ToClass<T>([NotNull] this object instance) where T : class
        {
            if (instance != null && instance is T)
            {
                return instance as T;
            }

            return null;
        }
    }
}