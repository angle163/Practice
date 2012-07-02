using System;
using Practice.Types.Annotation;

namespace Practice.Types.Interface
{
    /// <summary>
    /// The logger interface
    /// </summary>
    public interface ILogger
    {
        #region Properties

        /// <summary>
        /// Gets a value indicating whether IsDebugEnabled.
        /// </summary>
        bool IsDebugEnabled { get; }

        /// <summary>
        /// Gets a value indicating whether IsErrorEnabled.
        /// </summary>
        bool IsErrorEnabled { get; }

        /// <summary>
        /// Gets a value indicating whether IsFatalEnabled.
        /// </summary>
        bool IsFatalEnabled { get; }

        /// <summary>
        /// Gets a value indicating whether IsInfoEnabled.
        /// </summary>
        bool IsInfoEnabled { get; }

        /// <summary>
        /// Gets a value indicating whether IsTraceEnabled.
        /// </summary>
        bool IsTraceEnabled { get; }

        /// <summary>
        /// Gets a value indicating whether IsWarnEnabled.
        /// </summary>
        bool IsWarnEnabled { get; }

        /// <summary>
        /// Gets a value indicating the logging type.
        /// </summary>
        Type Type { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// The debug.
        /// </summary>
        /// <param name="format"> The format. </param>
        /// <param name="args"> The args. </param>
        void Debug([NotNull] string format, [NotNull] params object[] args);

        /// <summary>
        /// The debug.
        /// </summary>
        /// <param name="exception"> The exception. </param>
        /// <param name="format"> The format. </param>
        /// <param name="args"> The args. </param>
        void Debug([NotNull] Exception exception, [NotNull] string format, [NotNull] params object[] args);

        /// <summary>
        /// The error.
        /// </summary>
        /// <param name="format"> The format. </param>
        /// <param name="args"> The args. </param>
        void Error([NotNull] string format, [NotNull] params object[] args);

        /// <summary>
        /// The error.
        /// </summary>
        /// <param name="exception"> The exception. </param>
        /// <param name="format"> The format. </param>
        /// <param name="args"> The args. </param>
        void Error([NotNull] Exception exception, [NotNull] string format, [NotNull] params object[] args);

        /// <summary>
        /// The fatal.
        /// </summary>
        /// <param name="format"> The format. </param>
        /// <param name="args"> The args. </param>
        void Fatal([NotNull] string format, [NotNull] params object[] args);

        /// <summary>
        /// The fatal.
        /// </summary>
        /// <param name="exception"> The exception. </param>
        /// <param name="format"> The format. </param>
        /// <param name="args"> The args. </param>
        void Fatal([NotNull] Exception exception, [NotNull] string format, [NotNull] params object[] args);

        /// <summary>
        /// The info.
        /// </summary>
        /// <param name="format"> The format. </param>
        /// <param name="args"> The args. </param>
        void Info([NotNull] string format, [NotNull] params object[] args);

        /// <summary>
        /// The info.
        /// </summary>
        /// <param name="exception"> The exception. </param>
        /// <param name="format"> The format. </param>
        /// <param name="args"> The args. </param>
        void Info([NotNull] Exception exception, [NotNull] string format, [NotNull] params object[] args);

        /// <summary>
        /// The trace.
        /// </summary>
        /// <param name="format"> The format. </param>
        /// <param name="args"> The args. </param>
        void Trace([NotNull] string format, [NotNull] params object[] args);

        /// <summary>
        /// The trace.
        /// </summary>
        /// <param name="exception"> The exception. </param>
        /// <param name="format"> The format. </param>
        /// <param name="args"> The args. </param>
        void Trace([NotNull] Exception exception, [NotNull] string format, [NotNull] params object[] args);

        /// <summary>
        /// The warn.
        /// </summary>
        /// <param name="format"> The format. </param>
        /// <param name="args"> The args. </param>
        void Warn([NotNull] string format, [NotNull] params object[] args);

        /// <summary>
        /// The warn.
        /// </summary>
        /// <param name="exception"> The exception.
        /// </param>
        /// <param name="format"> The format. </param>
        /// <param name="args"> The args. </param>
        void Warn([NotNull] Exception exception, [NotNull] string format, [NotNull] params object[] args);

        #endregion
    }
}