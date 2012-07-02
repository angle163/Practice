using System.Web;
using Practice.Types;
using Practice.Types.Annotation;
using Practice.Types.Interface;

namespace Practice.Core
{
    /// <summary>
    /// The current http application provider.
    /// </summary>
    public class CurrentHttpApplicationStateBaseProvider : IReadWriteProvider<HttpApplicationStateBase>
    {
        #region Constants and Fields

        /// <summary>
        /// The _application.
        /// </summary>
        protected HttpApplicationStateBase _application;

        #endregion

        #region Properties

        /// <summary>
        ///   Gets or sets the Instance.
        /// </summary>
        [CanBeNull]
        public HttpApplicationStateBase Instance
        {
            get
            {
                if (_application == null && HttpContext.Current != null)
                {
                    _application = new HttpApplicationStateWrapper(HttpContext.Current.Application);
                }

                return _application;
            }

            set
            {
                CodeContract.ArgumentNotNull(value, "value");

                _application = value;
            }
        }

        #endregion
    }
}