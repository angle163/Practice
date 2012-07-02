using System.Threading;

namespace Practice.Pattern
{
    /// <summary>
    /// The read lock.
    /// </summary>
    public class ReadLock : BaseLock
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadLock"/> class.
        /// </summary>
        /// <param name="locks"> The locks. </param>
        public ReadLock(ReaderWriterLockSlim locks)
            : base(locks)
        {
            Locks.GetReadLock(_Locks);
        }


        /// <summary>
        /// The dispose.
        /// </summary>
        public override void Dispose()
        {
            Locks.ReleaseReadLock(_Locks);
        }
    }
}