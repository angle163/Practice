using System;
using System.Collections.Generic;
using Practice.Types.Annotation;

namespace Practice.Types.Interface
{
    /// <summary>
    /// The i service locator.
    /// </summary>
    public interface IServiceLocator : IServiceProvider
    {
        #region Public Methods

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="serviceType">
        /// The service type.
        /// </param>
        /// <returns>
        /// The get.
        /// </returns>
        object Get([NotNull] Type serviceType);

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="serviceType">
        /// The service type.
        /// </param>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        /// <returns>
        /// The get.
        /// </returns>
        object Get([NotNull] Type serviceType, [NotNull] IEnumerable<IServiceLocationParameter> parameters);

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="serviceType">
        /// The service type.
        /// </param>
        /// <param name="named">
        /// The named.
        /// </param>
        /// <returns>
        /// The get.
        /// </returns>
        object Get([NotNull] Type serviceType, [NotNull] string named);

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="serviceType">
        /// The service type.
        /// </param>
        /// <param name="named">
        /// The named.
        /// </param>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        /// <returns>
        /// The get.
        /// </returns>
        object Get([NotNull] Type serviceType, [NotNull] string named,
                   [NotNull] IEnumerable<IServiceLocationParameter> parameters);

        /// <summary>
        /// The try get.
        /// </summary>
        /// <param name="serviceType">
        /// The service type.
        /// </param>
        /// <param name="instance">
        /// The instance.
        /// </param>
        /// <returns>
        /// The try get.
        /// </returns>
        bool TryGet([NotNull] Type serviceType, [NotNull] out object instance);

        /// <summary>
        /// The try get.
        /// </summary>
        /// <param name="serviceType">
        /// The service type.
        /// </param>
        /// <param name="named">
        /// The named.
        /// </param>
        /// <param name="instance">
        /// The instance.
        /// </param>
        /// <returns>
        /// The try get.
        /// </returns>
        bool TryGet([NotNull] Type serviceType, [NotNull] string named, [NotNull] out object instance);

        #endregion
    }
}