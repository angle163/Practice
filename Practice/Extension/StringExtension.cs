using System;
using Practice.Types;
using Practice.Types.Annotation;

namespace Practice.Extension
{
    public static class StringExtension
    {
        /// <summary>
        /// Formats a string with the provided parameters.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="args">The args.</param>
        /// <returns>The formatted string.</returns>
        [StringFormatMethod("s")]
        public static string FormatWith(this string s, params object[] args)
        {
            return string.IsNullOrEmpty(s) ? null : string.Format(s, args);
        }

        /// <summary>
        /// When the string is trimmed, is it <see langword="null"/> or empty?
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>The is <see langword="null"/> or empty trimmed.</returns>
        [AssertionMethod]
        public static bool IsNotSet([AssertionCondition(AssertionConditionType.IsNull)] this string s)
        {
            return IsNullOrWhiteSpace(s);
        }

        /// <summary>
        /// When the string is trimed, is it <see langword="null"/> or empty?
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>The is <see langword="null"/> or emtpy trimmed.</returns>
        [AssertionMethod]
        public static bool IsSet([AssertionCondition(AssertionConditionType.IsNotNull)] this string s)
        {
            return !IsNullOrWhiteSpace(s);
        }

        /// <summary>
        /// 指示指定的字符串是 null、空还是仅由空白字符组成。
        /// </summary>
        /// <param name="value"> 要测试的字符串。</param>
        /// <returns> 如果 value 参数为 null 或 System.String.Empty，或者如果 value 仅由空白字符组成，则为 true。</returns>
        [AssertionMethod]
        private static bool IsNullOrWhiteSpace([AssertionCondition(AssertionConditionType.IsNotNull)] String value)
        {
            if (value == null) return true;

            for (int i = 0; i < value.Length; i++)
            {
                if (!Char.IsWhiteSpace(value[i])) return false;
            }

            return true;
        }

        /// <summary>
        /// Returns a "random" alpha-numeric string of specified length and characters.
        /// </summary>
        /// <param name="length">
        /// the length of the random string
        /// </param>
        /// <returns>
        /// The generate random string.
        /// </returns>
        public static string GenerateRandomString(int length)
        {
            return GenerateRandomString(length, "abcdefghijkmnpqrstuvwxyzABCDEFGHJKMNPQRSTUVWXYZ123456789");
        }

        /// <summary>
        /// Returns a "random" alpha-numeric string of specified length and characters.
        /// </summary>
        /// <param name="length">
        /// the length of the random string
        /// </param>
        /// <param name="pickText">
        /// the string of characters to pick randomly from
        /// </param>
        /// <returns>
        /// The generate random string.
        /// </returns>
        public static string GenerateRandomString(int length, [NotNull] string pickText)
        {
            CodeContract.ArgumentNotNull(pickText, "pickText");

            var r = new Random();
            var buf = new char[length];
            int pickLength = pickText.Length - 1;
            for (int i = 0; i < length; i++)
            {
                buf[i] = pickText[r.Next(pickLength)];
            }
            return new string(buf);
        }
    }
}