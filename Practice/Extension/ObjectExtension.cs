using System;
using System.ComponentModel;
using Practice.Types.Annotation;

namespace Practice.Extension
{
    /// <summary>
    /// The object extensions.
    /// </summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// Converts an object to Type using the Convert.ChangeType() call.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static T ToType<T>([CanBeNull] this object instance)
        {
            if (instance == null)
            {
                return default(T);
            }

            if (Equals(instance, default(T)))
            {
                return default(T);
            }

            if (Equals(instance, DBNull.Value))
            {
                return default(T);
            }

            Type instanceType = instance.GetType();

            if (instanceType == typeof (string))
            {
                if ((instance as string).IsNotSet())
                {
                    return default(T);
                }
            }

            var conversionType = typeof (T);

            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof (Nullable<>)))
            {
                conversionType = (new NullableConverter(conversionType)).UnderlyingType;
            }

            return (T) Convert.ChangeType(instance, conversionType);
        }
    }
}