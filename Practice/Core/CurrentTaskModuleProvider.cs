using System.Web;
using Practice.Types;
using Practice.Types.Annotation;
using Practice.Types.Constant;
using Practice.Types.Interface;

namespace Practice.Core
{
    /// <summary>
    /// The current task module provider.
    /// </summary>
    public class CurrentTaskModuleProvider : IReadWriteProvider<ITaskModuleManager>
    {
        #region Constants and Fields

        /// <summary>
        /// The _http application state.
        /// </summary>
        private readonly HttpApplicationStateBase _httpApplicationState;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentTaskModuleProvider"/> class.
        /// </summary>
        /// <param name="httpApplicationState">
        /// The http application state.
        /// </param>
        public CurrentTaskModuleProvider([NotNull] HttpApplicationStateBase httpApplicationState)
        {
            CodeContract.ArgumentNotNull(httpApplicationState, "httpApplicationState");

            _httpApplicationState = httpApplicationState;
        }

        #endregion

        #region Properties

        /// <summary>
        ///   The create.
        /// </summary>
        /// <returns>
        /// </returns>
        [CanBeNull]
        public ITaskModuleManager Instance
        {
            get
            {
                // Note: not treated with "BoardID" at all -- only one instance per application.
                return _httpApplicationState[Constant.Cache.TaskModule] as ITaskModuleManager;
            }

            set
            {
                CodeContract.ArgumentNotNull(value, "value");

                _httpApplicationState[Constant.Cache.TaskModule] = value;
            }
        }

        #endregion
    }
}