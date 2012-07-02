using System.Threading;

namespace Practice.Pattern
{
    /// <summary>
    /// The locks.
    /// </summary>
    public static class Locks
    {
        /// <summary>
        /// The get read lock.
        /// </summary>
        /// <param name="locks"> The locks. </param>
        public static void GetReadLock(ReaderWriterLockSlim locks)
        {
            bool lockAcquired = false;
            while (!lockAcquired)
            {
                lockAcquired = locks.TryEnterUpgradeableReadLock(1);
            }
        }

        /// <summary>
        /// The get read only lock.
        /// </summary>
        /// <param name="locks"> The locks. </param>
        public static void GetReadOnlyLock(ReaderWriterLockSlim locks)
        {
            bool lockAcquired = false;
            while (!lockAcquired)
            {
                lockAcquired = locks.TryEnterReadLock(1);
            }
        }

        /// <summary>
        /// The get write lock.
        /// </summary>
        /// <param name="locks"> The locks. </param>
        public static void GetWriteLock(ReaderWriterLockSlim locks)
        {
            bool lockAcquired = false;
            while (!lockAcquired)
            {
                lockAcquired = locks.TryEnterWriteLock(1);
            }
        }

        /// <summary>
        /// The release read only lock.
        /// </summary>
        /// <param name="locks"> The locks. </param>
        public static void ReleaseReadOnlyLock(ReaderWriterLockSlim locks)
        {
            if (locks.IsReadLockHeld)
            {
                locks.ExitReadLock();
            }
        }

        /// <summary>
        /// The release read lock.
        /// </summary>
        /// <param name="locks"> The locks. </param>
        public static void ReleaseReadLock(ReaderWriterLockSlim locks)
        {
            if (locks.IsUpgradeableReadLockHeld)
            {
                locks.ExitUpgradeableReadLock();
            }
        }

        /// <summary>
        /// The release write lock.
        /// </summary>
        /// <param name="locks"> The locks. </param>
        public static void ReleaseWriteLock(ReaderWriterLockSlim locks)
        {
            if (locks.IsWriteLockHeld)
            {
                locks.ExitWriteLock();
            }
        }

        /// <summary>
        /// The release lock.
        /// </summary>
        /// <param name="locks"> The locks. </param>
        public static void ReleaseLock(ReaderWriterLockSlim locks)
        {
            ReleaseWriteLock(locks);
            ReleaseReadLock(locks);
            ReleaseReadOnlyLock(locks);
        }

        /// <summary>
        /// The get lock instance.
        /// </summary>
        /// <returns></returns>
        public static ReaderWriterLockSlim GetLockInstance()
        {
            return GetLockInstance(LockRecursionPolicy.SupportsRecursion);
        }

        /// <summary>
        /// The get lock instance.
        /// </summary>
        /// <param name="recursionPolicy">
        /// The recursion policy.
        /// </param>
        /// <returns></returns>
        public static ReaderWriterLockSlim GetLockInstance(LockRecursionPolicy recursionPolicy)
        {
            return new ReaderWriterLockSlim(recursionPolicy);
        }
    }
}