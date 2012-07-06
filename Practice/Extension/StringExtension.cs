using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Practice.Types;
using Practice.Types.Annotation;

namespace Practice.Extension
{
    /// <summary>
    /// 字符串方法的扩张类。
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 将指定字符串中的格式项替换为指定数组中相应对象的字符串表示形式。
        /// </summary>
        /// <param name="s"> 复合格式字符串。 </param>
        /// <param name="args"> 一个对象数组，其中包含零个或多个要设置格式的对象。 </param>
        /// <returns> format 的副本，其中的格式项已替换为 args 中相应对象的字符串表示形式。 </returns>
        /// <exception cref="System.ArgumentNullException"> format 或 args 为 null。 </exception>
        /// <exception cref="System.FormatException">
        /// format 无效。- 或 -格式项的索引小于零或大于等于 args 数组的长度。
        /// </exception>
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
        /// 获取一个指定长度的随机字符串。
        ///     其中内容包括英文字母和数字，除开l,o和0字符。
        /// </summary>
        /// <param name="length"> 随机字符串的长度。 </param>
        /// <returns> 生成的随机字符串。 </returns>
        public static string GenerateRandomString(int length)
        {
            return GenerateRandomString(length, "abcdefghijkmnpqrstuvwxyzABCDEFGHJKMNPQRSTUVWXYZ123456789");
        }

        /// <summary>
        /// 获取一个指定长度的随机字符串，其中内容为指定字符串中的字符。
        /// </summary>
        /// <param name="length"> 随机字符串的长度。 </param>
        /// <param name="pickText"> 提供随机字符的源字符串。 </param>
        /// <returns> 生成的随机字符串。 </returns>
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

        /// <summary>
        /// 将字符串转换为16进制的字符串。
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string StringToHexBytes(this string s)
        {
            string value = string.Empty;
            if (s.IsNotSet())
            {
                return value;
            }

            var cryptoServiceProvider = new MD5CryptoServiceProvider();
            var bytes = Encoding.UTF8.GetBytes(s);
            bytes = cryptoServiceProvider.ComputeHash(bytes);
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("x2").ToLower());
            }

            return sb.ToString();
        }

        /// <summary>
        /// 将字符转换为一个编码的JavaScript字符串。
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToJsString([CanBeNull] this string s)
        {
            if (!s.IsNotSet())
            {
                return s;
            }

            // 将字符串中的
            //      斜线(\)、引号(')、双引号(")、换行符(\n)、回车符(\r)
            // 替换为转义字符
            s = s.Replace("\\", @"\\");
            s = s.Replace("'", @"\'");
            s = s.Replace("\"", @"\""");
            s = s.Replace("\r", @"\r");
            s = s.Replace("\n", @"\n");

            return s;
        }

        /// <summary>
        /// 校验每一个单词的长度是否超过指定的最大长度。
        /// </summary>
        /// <param name="s"></param>
        /// <param name="maxLen"></param>
        /// <returns></returns>
        public static bool AreAnyWordsOverMaxLength([NotNull] this string s, int maxLen)
        {
            CodeContract.ArgumentNotNull(s, "s");

            return maxLen > 0 && s.Length > 0
                    && s.Split(' ').Any(w => w.IsSet() && w.Length > maxLen);
        }
    }
}