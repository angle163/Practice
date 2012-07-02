using Practice.Types.Interface;

namespace Practice.Types
{
    /// <summary>
    /// The service locator access -- kind of a hack. Will not be needed in the future.
    /// </summary>
    public class ServiceLocatorAccess : IHaveServiceLocator
    {
        #region Properties

        /// <summary>
        /// Gets or sets CurrentServiceProvider.
        /// </summary>
        public static IServiceLocator CurrentServiceProvider { get; set; }

        /// <summary>
        ///   Gets ServiceLocator.
        /// </summary>
        public IServiceLocator ServiceLocator
        {
            get { return CurrentServiceProvider; }
        }

        #endregion
    }
}