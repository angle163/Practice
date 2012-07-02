using System;
using Autofac;
using Autofac.Core;
using Practice.Types;
using Practice.Types.Annotation;
using Practice.Types.Interface;

namespace Practice.Extension
{
    /// <summary>
    /// The i have component registry extensions.
    /// </summary>
    public static class HaveComponentRegistryExtension
    {
        #region Public Methods

        /// <summary>
        /// Is not registered in the component registry.
        /// </summary>
        /// <param name="haveComponentRegistry">
        /// The have component registry.
        /// </param>
        /// <typeparam name="TRegistered">
        /// </typeparam>
        /// <returns>
        /// The is registered.
        /// </returns>
        public static bool IsNotRegistered<TRegistered>([NotNull] this IHaveComponentRegistry haveComponentRegistry)
        {
            CodeContract.ArgumentNotNull(haveComponentRegistry, "haveComponentRegistry");

            return !haveComponentRegistry.ComponentRegistry.IsRegistered(new TypedService(typeof(TRegistered)));
        }

        /// <summary>
        /// Is not registered in the component registry.
        /// </summary>
        /// <param name="haveComponentRegistry">
        /// The have component registry.
        /// </param>
        /// <param name="registeredType"></param>
        /// <returns>
        /// The is registered.
        /// </returns>
        public static bool IsNotRegistered([NotNull] this IHaveComponentRegistry haveComponentRegistry, Type registeredType)
        {
            CodeContract.ArgumentNotNull(haveComponentRegistry, "haveComponentRegistry");

            return !haveComponentRegistry.ComponentRegistry.IsRegistered(new TypedService(registeredType));
        }

        /// <summary>
        /// The is registered.
        /// </summary>
        /// <param name="haveComponentRegistry">
        /// The have component registry.
        /// </param>
        /// <param name="registeredType"></param>
        /// <returns>
        /// The is registered.
        /// </returns>
        public static bool IsRegistered([NotNull] this IHaveComponentRegistry haveComponentRegistry, Type registeredType)
        {
            CodeContract.ArgumentNotNull(haveComponentRegistry, "haveComponentRegistry");

            return haveComponentRegistry.ComponentRegistry.IsRegistered(new TypedService(registeredType));
        }

        /// <summary>
        /// The is registered.
        /// </summary>
        /// <param name="haveComponentRegistry">
        /// The have component registry.
        /// </param>
        /// <typeparam name="TRegistered">
        /// </typeparam>
        /// <returns>
        /// The is registered.
        /// </returns>
        public static bool IsRegistered<TRegistered>([NotNull] this IHaveComponentRegistry haveComponentRegistry)
        {
            CodeContract.ArgumentNotNull(haveComponentRegistry, "haveComponentRegistry");

            return haveComponentRegistry.ComponentRegistry.IsRegistered(new TypedService(typeof(TRegistered)));
        }

        /// <summary>
        /// Is not registered in the component registry.
        /// </summary>
        /// <param name="haveComponentRegistry">
        /// The have component registry.
        /// </param>
        /// <typeparam name="TRegistered">
        /// </typeparam>
        /// <returns>
        /// The is registered.
        /// </returns>
        public static bool IsNotRegistered<TRegistered>([NotNull] this IHaveComponentRegistry haveComponentRegistry, string named)
        {
            CodeContract.ArgumentNotNull(haveComponentRegistry, "haveComponentRegistry");

            return !haveComponentRegistry.ComponentRegistry.IsRegistered(new KeyedService(named, typeof(TRegistered)));
        }

        /// <summary>
        /// The is registered.
        /// </summary>
        /// <param name="haveComponentRegistry">
        /// The have component registry.
        /// </param>
        /// <typeparam name="TRegistered">
        /// </typeparam>
        /// <returns>
        /// The is registered.
        /// </returns>
        public static bool IsRegistered<TRegistered>([NotNull] this IHaveComponentRegistry haveComponentRegistry, string named)
        {
            CodeContract.ArgumentNotNull(haveComponentRegistry, "haveComponentRegistry");

            return haveComponentRegistry.ComponentRegistry.IsRegistered(new KeyedService(named, typeof(TRegistered)));
        }

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="haveComponentRegistry">
        /// The have component registry.
        /// </param>
        /// <param name="containerBuilder">
        /// The container builder.
        /// </param>
        public static void UpdateRegistry(
          [NotNull] this IHaveComponentRegistry haveComponentRegistry, [NotNull] ContainerBuilder containerBuilder)
        {
            CodeContract.ArgumentNotNull(haveComponentRegistry, "haveComponentRegistry");
            CodeContract.ArgumentNotNull(containerBuilder, "containerBuilder");

            containerBuilder.Update(haveComponentRegistry.ComponentRegistry);
        }

        #endregion
    }
}