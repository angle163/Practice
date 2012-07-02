namespace Practice.Types.Flag
{
    /// <summary>
    /// User flag manipulation class from the DB.
    /// </summary>
    public class UserFlag : FlagBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserFlag"/> class.
        /// </summary>
        public UserFlag()
            : this(0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserFlag"/> class.
        /// </summary>
        /// <param name="flag">The flag.</param>
        public UserFlag(Flag flag)
            : this((int) flag)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserFlag"/> class.
        /// </summary>
        /// <param name="bitValue">The bit value.</param>
        public UserFlag(object bitValue)
            : this((int) bitValue)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserFlag"/> class.
        /// </summary>
        /// <param name="bitValue">
        /// The bit value.
        /// </param>
        public UserFlag(int bitValue)
            : base(bitValue)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserFlag"/> class.
        /// </summary>
        /// <param name="bits">The bits.</param>
        public UserFlag(params bool[] bits)
            : base(bits)
        {
        }

        #region Single Flag (can be 32 of them)

        /// <summary>
        /// Gets or sets a value indicating whether the user is host administrator.
        /// </summary>
        public bool IsHostAdmin
        {
            // int value 1
            get { return this[0]; }
            set { this[0] = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether is approved for posting.
        /// </summary>
        public bool IsApproved
        {
            // int value 2
            get { return this[1]; }
            set { this[1] = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether user is guest, i.e. not registered and logged in.
        /// </summary>
        public bool IsGuest
        {
            // int value 4
            get { return this[2]; }
            set { this[2] = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether user is guest, i.e. not registered and logged in.
        /// </summary>
        public bool IsCaptchaExcluded
        {
            // int value 8
            get { return this[3]; }
            set { this[3] = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether user is excluded from the "Active Users" list on the forum pages.
        /// </summary>
        public bool IsActiveExcluded
        {
            // int value 16
            get { return this[4]; }
            set { this[4] = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a user is enabled the DST correction.
        /// </summary>
        public bool IsDST
        {
            // int value 32
            get { return this[5]; }
            set { this[5] = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a user profile/personal data was changed. 
        /// The flag is set every time when a user profile changes.
        /// Used for portal intgration.
        /// </summary>
        public bool IsDirty
        {
            // int value 64
            get { return this[6]; }
            set { this[6] = value; }
        }

        #endregion

        #region Operators

        /// <summary>
        /// The operator implicit.
        /// </summary>
        /// <param name="newBitValue">The new bit value.</param>
        /// <returns></returns>
        public static implicit operator UserFlag(int newBitValue)
        {
            return new UserFlag(newBitValue);
        }

        /// <summary>
        /// The operator implicit.
        /// </summary>
        /// <param name="flag">The flag.</param>
        /// <returns></returns>
        public static implicit operator UserFlag(Flag flag)
        {
            return new UserFlag(flag);
        }

        #endregion
    }
}