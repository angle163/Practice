namespace Practice.Types.Interface
{
    /// <summary>
    /// The i have service locator -- objects that have a reference to the service locator.
    /// </summary>
    public interface IHaveServiceLocator
    {
        #region Properties

        /// <summary>
        /// Gets ServiceLocator.
        /// </summary>
        IServiceLocator ServiceLocator { get; }

        #endregion
    }
}