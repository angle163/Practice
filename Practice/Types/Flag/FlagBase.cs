#region Using directives

using System;
using Practice.Types.Annotation;

#endregion

namespace Practice.Types.Flag
{
    /// <summary>
    /// FlagBase的摘要:
    ///     各种标识实现基础的抽象类。
    /// </summary>
    [Serializable]
    public abstract class FlagBase
    {
        #region Constants and Fields

        /// <summary>
        /// 标识位的<see cref="int"/>值，最多记录64个位。
        /// </summary>
        protected int _bitValue;

        #endregion

        #region Constructors and Destructros

        /// <summary>
        /// 初始化<see cref="FlagBase"/>类的新实例，
        /// 并将它的标识位参数设置为默认值。
        /// </summary>
        public FlagBase()
            : this(0)
        {
        }

        /// <summary>
        /// 初始化<see cref="FlagBase"/>类的新实例，
        /// 并用指定的<see cref="int"/>参数设置其标识位参数的值。
        /// </summary>
        /// <param name="bitValue"> 初始化标识位的<see cref="int"/>类型值。 </param>
        public FlagBase(int bitValue)
        {
            _bitValue = bitValue;
        }

        /// <summary>
        /// 初始化<see cref="FlagBase"/>类的新实例，
        /// 并根据指定的布尔值数组设置其标识位参数的值。
        /// </summary>
        /// <param name="bits">
        /// 新标识位参数的所表示布尔值数组。
        ///      若数组长度小于32，其余位将设置为<c>false</c>；若数组长度大于32，多余的元素会别忽略。
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
        /// 获取或设置标识位的<see cref="int"/>值。
        /// </summary>
        public int BitValue
        {
            get { return _bitValue; }
            set { _bitValue = value; }
        }

        #endregion

        #region Indexsers

        /// <summary>
        /// 获取或设置在标志位中指定索引位置的标识所表示的布尔值。
        /// </summary>
        /// <param name="index"> 基于0的索引。 </param>
        /// <returns>
        /// 返回指定在标志位中指定索引位置的标识所表示的布尔值。
        ///     若从未设置过指定索引位置的标识所表示的布尔值，返回<c>false</c>.
        /// </returns>
        public bool this[int index]
        {
            get { return GetBitAsBool(_bitValue, index); }
            set { _bitValue = SetBitFromBool(_bitValue, index, value); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 获取指定标识位中指定位数的标识所表示的布尔值。
        /// </summary>
        /// <param name="bitValue"> 标识位的<see cref="int"/>值。 </param>
        /// <param name="bitShift"> 基于0的位数。 </param>
        /// <returns> 返回布尔值。 </returns>
        public static bool GetBitAsBool(int bitValue, int bitShift)
        {
            if (bitShift > 63)
            {
                bitShift %= 63;
            }

            return ((bitValue >> bitShift) & 0x1) == 1;
        }

        /// <summary>
        /// 将指定布尔值，设置指定标识位中指定位数所在标识所表示的布尔值。
        /// </summary>
        /// <param name="bitValue">  标识位的<see cref="int"/>值。 </param>
        /// <param name="bitShift"> 基于0的位数。 </param>
        /// <param name="value"> 新标识所表示的布尔值。 </param>
        /// <returns> 返回新标志位的<see cref="int"/>值。 </returns>
        public static int SetBitFromBool(int bitValue, int bitShift, bool value)
        {
            if (bitShift > 63)
            {
                bitShift %= 63;
            }

            if (GetBitAsBool(bitValue, bitShift) != value)
            {
                // toggle that value using XOR.
                bitValue ^= 0x1 << bitShift;
            }

            return bitValue;
        }


        /// <summary>
        /// 转换标识枚举为标志位中标志的索引。
        ///     正确索引范围在0～63之间，若返回-1表示转换失败。
        /// </summary>
        /// <param name="theEnum"> 标识枚举。 </param>
        /// <returns> 若转换成功则返回索引，否则返回-1。 </returns>
        public static int EnumToIndex([NotNull] Enum theEnum)
        {
            CodeContract.ArgumentNotNull(theEnum, "theEnum");

            int flagBit = Convert.ToInt32(theEnum);
            for (int i = 0; i < 63; i++)
            {
                if (((flagBit >> i) & 0x1) == 1)
                {
                    return i;
                }
            }

            return -1;
        }

        #endregion
    }
}