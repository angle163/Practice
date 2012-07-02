using System.Threading;

namespace Practice.Pattern
{
    /// <summary>
    /// The write lock.
    /// </summary>
    public class WriteLock : BaseLock
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WriteLock"/> class.
        /// </summary>
        /// <param name="locks"> The locks. </param>
        public WriteLock(ReaderWriterLockSlim locks)
            : base(locks)
        {
            Locks.GetWriteLock(_Locks);
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public override void Dispose()
        {
            Locks.ReleaseWriteLock(_Locks);
        }
    }
}