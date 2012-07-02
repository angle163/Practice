using System;
using System.Web;
using System.Web.Caching;
using Practice.Core;
using Practice.Extension;
using Practice.Types;
using Practice.Types.Annotation;

namespace Practice.Helper
{
    /// <summary>
    /// The captcha helper.
    /// </summary>
    public class CaptchaHelper
    {
        /// <summary>
        /// Gets the CaptchaString.
        /// </summary>
        public static string CaptchaString
        {
            get { return StringExtension.GenerateRandomString(6); }
        }

        /// <summary>
        /// The get captcha text.
        /// </summary>
        /// <param name="session">
        /// </param>
        /// <param name="cache">
        /// The cache.
        /// </param>
        /// <param name="forceNew">
        /// The force New.
        /// </param>
        /// <returns>
        /// The get captcha text.
        /// </returns>
        public static string GetCaptchaText([NotNull] HttpSessionStateBase session, [NotNull] Cache cache, bool forceNew)
        {
            CodeContract.ArgumentNotNull(session, "session");
            var cacheName = "Session{0}CaptchaImageText".FormatWith(session.SessionID);
            if (!forceNew && cache[cacheName] != null)
            {
                return cache[cacheName].ToString();
            }

            var text = CaptchaString;
            if (cache[cacheName] != null)
            {
                cache[cacheName] = text;
            }
            else
            {
                cache.Add(
                    cacheName, text, null,
                    Cache.NoAbsoluteExpiration,
                    TimeSpan.FromMinutes(10),
                    CacheItemPriority.Low, null);
            }
            return text;
        }

        /// <summary>
        /// The is valid.
        /// </summary>
        /// <param name="captchaText">
        /// The captcha text.
        /// </param>
        /// <returns>
        /// The is valid.
        /// </returns>
        public static bool IsValid([NotNull] string captchaText)
        {
            CodeContract.ArgumentNotNull(captchaText, "captchaText");
            var text = GetCaptchaText(
                FakeContext.Current.Get<HttpSessionStateBase>(),
                HttpRuntime.Cache, false);
            return string.Compare(
                text, captchaText,
                StringComparison.InvariantCultureIgnoreCase) == 0;
        }
    }
}