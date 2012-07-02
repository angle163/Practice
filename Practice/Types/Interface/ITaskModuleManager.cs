using System;
using System.Collections.Generic;
using System.Web;
using Practice.Types.Annotation;

namespace Practice.Types.Interface
{
    /// <summary>
    /// The i task module manager.
    /// </summary>
    public interface ITaskModuleManager
    {
        #region Properties

        /// <summary>
        ///   Gets TaskCount.
        /// </summary>
        int TaskCount { get; }

        /// <summary>
        ///   All the names of tasks running.
        /// </summary>
        IList<string> TaskManagerInstances { get; }

        /// <summary>
        ///   Gets TaskManagerSnapshot.
        /// </summary>
        IDictionary<string, IBackgroundTask> TaskManagerSnapshot { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Check if a Task is Running.
        /// </summary>
        /// <param name="instanceName"></param>
        /// <returns> The is task running. </returns>
        bool IsTaskRunning([NotNull] string instanceName);

        /// <summary>
        /// Start a non-running task -- will set the <see cref="HttpApplication"/> instance.
        /// </summary>
        /// <param name="instanceName">
        /// Unique name of this task
        /// </param>
        /// <param name="startTask"> Task to run </param>
        bool StartTask([NotNull] string instanceName, Func<IBackgroundTask> startTask);

        /// <summary>
        /// The stop task.
        /// </summary>
        /// <param name="instanceName"> The instance name. </param>
        void StopTask([NotNull] string instanceName);

        /// <summary>
        /// Attempt to get the instance of the task.
        /// </summary>
        /// <param name="instanceName"></param>
        /// <returns></returns>
        bool TryGetTask([NotNull] string instanceName, out IBackgroundTask task);

        /// <summary>
        /// The try remove task.
        /// </summary>
        /// <param name="instanceName"> The instance name. </param>
        /// <returns> The try remove task. </returns>
        bool TryRemoveTask([NotNull] string instanceName);

        #endregion
    }
}