using Practice.Types;
using Practice.Types.Annotation;
using Practice.Types.Interface;

namespace Practice.Extension
{
    /// <summary>
    /// The interface service locator extensions.
    /// </summary>
    public static class ServiceLocatorExtension
    {
        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="serviceLocator">
        /// The service locator.
        /// </param>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static TService Get<TService>([NotNull] this IServiceLocator serviceLocator)
        {
            CodeContract.ArgumentNotNull(serviceLocator, "serviceLocator");
            return (TService)serviceLocator.Get(typeof(TService));
        }

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="haveLocator">
        /// The have locator.
        /// </param>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static TService Get<TService>([NotNull] this IHaveServiceLocator haveLocator)
        {
            CodeContract.ArgumentNotNull(haveLocator, "haveLocator");
            return haveLocator.ServiceLocator.Get<TService>();
        }
    }
}