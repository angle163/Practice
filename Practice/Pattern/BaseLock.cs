using System;
using System.Threading;

namespace Practice.Pattern
{
    /// <summary>
    /// The base lock.
    /// </summary>
    public abstract class BaseLock : IDisposable
    {
        /// <summary>
        /// The _ locks.
        /// </summary>
        protected ReaderWriterLockSlim _Locks;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseLock"/> class.
        /// </summary>
        /// <param name="locks"> The locks. </param>
        public BaseLock(ReaderWriterLockSlim locks)
        {
            _Locks = locks;
        }

        #region IDisposable Members

        /// <summary>
        /// The dispose.
        /// </summary>
        public abstract void Dispose();

        #endregion
    }
}