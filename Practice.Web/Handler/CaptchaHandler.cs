using System.Drawing.Imaging;
using System.Web;
using System.Web.SessionState;
using Practice.Core;
using Practice.Helper;
using Practice.Types.Annotation;
using Practice.Types.Interface;
using Practice.Util;

namespace Practice.Web.Handler
{
    public class CaptchaHandler : IHttpHandler, IReadOnlySessionState, IHaveServiceLocator
    {
        public void ProcessRequest(HttpContext context)
        {
            GetResponseCaptcha(context);
        }

        public bool IsReusable
        {
            get { return false; }
        }

        public IServiceLocator ServiceLocator
        {
            get { return FakeContext.Current.ServiceLocator; }
        }

        /// <summary>
        /// The get response captcha.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        private void GetResponseCaptcha([NotNull] HttpContext context)
        {
#if (!DEBUG)
            try
            {
#endif
                var captchaImage = new CaptchaImage(
                  CaptchaHelper.GetCaptchaText(new HttpSessionStateWrapper(context.Session), context.Cache, true)
                  , 250, 50, "Century Schoolbook");
                context.Response.Clear();
                context.Response.ContentType = "image/jpeg";
                captchaImage.Image.Save(context.Response.OutputStream, ImageFormat.Jpeg);
#if (!DEBUG)
            }
            catch (Exception x)
            {
                LegacyDb.eventlog_create(null, this.GetType().ToString(), x, 1);
                context.Response.Write("Error: Resource has been moved or is unavailable. Please contact the forum admin.");
            }
#endif
        }
    }
}