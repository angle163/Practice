using System;
using Practice.Types.Annotation;

namespace Practice.Types.Interface
{
    /// <summary>
    /// The i logger provider.
    /// </summary>
    public interface ILoggerProvider
    {
        #region Public Methods

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="type"> The type. </param>
        /// <returns></returns>
        ILogger Create([CanBeNull] Type type);

        #endregion
    }
}