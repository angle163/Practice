using Autofac;
using Practice.Core.Modules;
using Practice.Types;
using Practice.Types.Interface;

namespace Practice.Core
{
    /// <summary>
    /// Instance of the Global Container... yes, a God class. It's the best way to do it, though.
    /// </summary>
    public static class GlobalContainer
    {
        #region Constants and Fields

        /// <summary>
        /// The _sync object.
        /// </summary>
        private static readonly object _syncObject = new object();

        /// <summary>
        ///   The _container.
        /// </summary>
        private static IContainer _container;

        #endregion

        #region Properties

        /// <summary>
        ///   Gets Container.
        /// </summary>
        public static IContainer Container
        {
            get { return _container ?? CreateContainer(); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The create container.
        /// </summary>
        private static IContainer CreateContainer()
        {
            lock (_syncObject)
            {
                if (_container == null)
                {
                    _container = BuliderContainer();

                    // immediately setup the static service locator...
                    ServiceLocatorAccess.CurrentServiceProvider = _container.Resolve<IServiceLocator>();
                }
            }
            return _container;
        }

        private static IContainer BuliderContainer()
        {
            var builder = new ContainerBuilder();

            var mainModule = new FakeBaseContainerModule();

            builder.RegisterModule(mainModule);

            return builder.Build();
        }

        #endregion
    }
}