using System;
using System.Web;

namespace Practice.Pattern
{
    /// <summary>
    /// Singleton factory implementation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageSingleton<T>
        where T : class, new()
    {
        // static constructor,
        // runtime ensures thread safety.

        #region Constants and Fields

        /// <summary>
        /// The _instance.
        /// </summary>
        private static T _instance;

        #endregion

        #region Properties

        /// <summary>
        /// Gets Instance.
        /// </summary>
        public static T Instance
        {
            get { return GetInstance(); }
            private set { _instance = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>The instance of T.</returns>
        private static T GetInstance()
        {
            if (HttpContext.Current == null)
            {
                return _instance ?? (_instance = (T) Activator.CreateInstance(typeof (T)));
            }

            string typeStr = typeof (T).ToString();
            return (T)
                   (
                       HttpContext.Current.Items[typeStr]
                       ?? (HttpContext.Current.Items[typeStr] = Activator.CreateInstance(typeof (T)))
                   );
        }

        #endregion
    }
}