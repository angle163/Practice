using System;
using Practice.Types.Annotation;

namespace Practice.Types.Interface
{
    /// <summary>
    /// The interface that marks a container with support for injection into an object.
    /// </summary>
    public interface IInjectService
    {
        #region Public Methods

        /// <summary>
        /// Inject an object with services.
        /// </summary>
        /// <typeparam name="TAttribute">
        /// TAttribute is the attribute that marks properties to inject to.
        /// </typeparam>
        /// <param name="instance"> The object to inject. </param>
        void InjectMarked<TAttribute>([NotNull] object instance) where TAttribute : Attribute;

        #endregion
    }
}