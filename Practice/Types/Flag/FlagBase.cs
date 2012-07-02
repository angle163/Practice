#region Using directives

using System;
using Practice.Types.Annotation;

#endregion

namespace Practice.Types.Flag
{
    /// <summary>
    /// Abstract class as a foundation for various flag implementations.
    /// </summary>
    [Serializable]
    public class FlagBase
    {
        #region Constants and Fields

        /// <summary>
        /// Integer value stores up to 64 flag/bit.
        /// </summary>
        protected int _bitValue;

        #endregion

        #region Constructors and Destructros

        /// <summary>
        /// Initializes a new instance of the <see cref="FlagBase"/> class.
        ///     Create new instance and initialize it with value of bitValue parameter.
        /// </summary>
        public FlagBase()
            : this(0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FlagBase"/> class.
        ///     Create new instance and initialize it with value of bitValue parameter.
        /// </summary>
        /// <param name="bitValue">Initialize integer value.</param>
        public FlagBase(int bitValue)
        {
            _bitValue = bitValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FlagBase"/> class.
        ///     Create new instance and initialize it with bits set accoding to param array.
        /// </summary>
        /// <param name="bits">
        /// Boolean values to initialize class with. If their number is less than 32, remaining bits
        /// are set to false. If greater than 32 value is specified, excess values are ignored.
        /// </param>
        public FlagBase([NotNull] params bool[] bits)
            : this(0)
        {
            int minLength = Math.Min(bits.Length, 31);
            // process up to 32 parameters.
            for (int i = 0; i < minLength; i++)
            {
                // set this bit
                this[i] = bits[i];
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets integer value of flag.
        /// </summary>
        public int BitValue
        {
            get { return _bitValue; }
            set { _bitValue = value; }
        }

        #endregion

        #region Indexsers

        /// <summary>
        /// Gets or sets bit at position specified by index.
        /// </summary>
        /// <param name="index"> Zero-based index of bit to get or set. </param>
        /// <returns>
        /// Boolean value indicating whether bit at position specified by index is set or not.
        /// </returns>
        public bool this[int index]
        {
            get { return GetBitAsBool(_bitValue, index); }
            set { _bitValue = SetBitFromBool(_bitValue, index, value); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets boolean indicating whether bit on bitShift position in bitValue integer is set or not.
        /// </summary>
        /// <param name="bitValue">Integer value.</param>
        /// <param name="bitShift"> Zero-based position of bit to get. </param>
        /// <returns>
        /// Returns boolean indicating whether bit at bitShift position is set or not.
        /// </returns>
        private bool GetBitAsBool(int bitValue, int bitShift)
        {
            if (bitShift > 63)
            {
                bitShift %= 63;
            }

            return ((bitValue >> bitShift) & 1) == 1;
        }

        /// <summary>
        /// Sets or unsets bit of bitValue integer at position specified by bitShift, depending on value parameter.
        /// </summary>
        /// <param name="bitValue">Integer value.</param>
        /// <param name="bitShift"> Zero-based position of bit to set. </param>
        /// <param name="value"> New boolean value of bit. </param>
        /// <returns>
        /// Returns new integer value with bit at position specified by bitShift parameter set to value.
        /// </returns>
        private int SetBitFromBool(int bitValue, int bitShift, bool value)
        {
            if (bitShift > 63)
            {
                bitShift %= 63;
            }

            if (GetBitAsBool(bitValue, bitShift) != value)
            {
                // toggle that value using XOR.
                bitValue ^= 1 << bitShift;
            }

            return bitValue;
        }


        /// <summary>
        /// Converts a Flag Enum to the associated index value.
        /// </summary>
        /// <param name="theEnum">The Flag Enum.</param>
        /// <returns>The enum to index.</returns>
        public int EnumToIndex([NotNull] Enum theEnum)
        {
            CodeContract.ArgumentNotNull(theEnum, "theEnum");
            return Convert.ToInt32(Math.Sqrt(Convert.ToInt32(theEnum))) - 1;
        }

        #endregion
    }
}