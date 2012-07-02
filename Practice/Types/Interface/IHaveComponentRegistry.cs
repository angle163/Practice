using Autofac.Core;

namespace Practice.Types.Interface
{
    /// <summary>
    /// The i have component registry.
    /// </summary>
    public interface IHaveComponentRegistry
    {
        #region Properties

        /// <summary>
        /// Gets or sets ComponentRegistry.
        /// </summary>
        IComponentRegistry ComponentRegistry { get; set; }

        #endregion
    }
}