using System.Threading;

namespace Practice.Pattern
{
    /// <summary>
    /// The read only lock.
    /// </summary>
    public class ReadOnlyLock : BaseLock
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyLock"/> class.
        /// </summary>
        /// <param name="locks"> The locks. </param>
        public ReadOnlyLock(ReaderWriterLockSlim locks)
            : base(locks)
        {
            Locks.GetReadOnlyLock(_Locks);
        }


        /// <summary>
        /// The dispose.
        /// </summary>
        public override void Dispose()
        {
            Locks.ReleaseReadOnlyLock(_Locks);
        }
    }
}