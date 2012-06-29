using System;

namespace Practice.Types
{
    /// <summary>
    /// 提供用于代码协定的函数的静态类。
    /// </summary>
    public static class CodeContract
    {
        /// <summary>
        /// 验证指定参数(obj)是否为空。如果为空，则抛出异常。
        /// </summary>
        /// <typeparam name="T">需要验证参数(obj)的类型。</typeparam>
        /// <param name="obj">需要验证的参数(obj)。</param>
        /// <param name="argumentName">需要验证参数(obj)的名称。</param>
        public static void ArgumentNotNull<T>(T obj, string argumentName) where T : class
        {
            if (obj == null)
            {
                throw new ArgumentNullException(argumentName, string.Format("{0} can not be null.", argumentName));
            }
        }
    }
}