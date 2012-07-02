#region Using directives.

using System;

#endregion

namespace Practice.Types.Flag
{
    #region Flag Enumeration

    /// <summary>
    /// User for bit comparisons.
    /// </summary>
    [Flags]
    public enum Flag
    {
        /// <summary>
        /// None flag.
        /// </summary>
        None = 0,

        /// <summary>
        /// The is host admin.
        /// </summary>
        IsHostAdmin = 1,

        /// <summary>
        /// The is approved.
        /// </summary>
        IsApproved = 2,

        /// <summary>
        /// The is guest.
        /// </summary>
        IsGuest = 4,

        /// <summary>
        /// The is captcha excluded.
        /// </summary>
        IsCaptchaExcluded = 8,

        /// <summary>
        /// The is active excluded.
        /// </summary>
        IsActiveExcluded = 16,

        /// <summary>
        /// The Daylight Saving Time is enabled.
        /// </summary>
        IsDaylightSavingTime = 32,

        /// <summary>
        /// Is Dirty data flag.
        /// </summary>
        IsDirty = 64

        /* for futrue use
         *  xxxx = 128
         *  xxxx = 256
         *  xxxx = 512
         *  */
    }

    #endregion
}