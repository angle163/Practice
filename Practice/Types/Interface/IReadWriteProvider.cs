namespace Practice.Types.Interface
{
    /// <summary>
    /// The read and wite provider.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IReadWriteProvider<T>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Instance.
        /// </summary>
        T Instance { get; set; }

        #endregion
    }
}