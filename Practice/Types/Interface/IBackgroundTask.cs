using System;

namespace Practice.Types.Interface
{
    /// <summary>
    /// The background task interface
    /// </summary>
    public interface IBackgroundTask : IDisposable
    {
        /// <summary>
        /// Sets Data
        /// </summary>
        object Data { set; }

        /// <summary>
        /// Gets Started.
        /// </summary>
        DateTime Started { get; }

        /// <summary>
        /// Gets a value indicating whether IsRunning.
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// The run.
        /// </summary>
        void Run();
    }
}