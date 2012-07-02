using System.Web;
using System.Web.SessionState;
using Practice.Core;
using Practice.Types.Interface;

namespace Practice.Web.Handler
{
    public class ResourceHandler : IHttpHandler, IReadOnlySessionState, IHaveServiceLocator
    {
        public void ProcessRequest(HttpContext context)
        {
            throw new System.NotImplementedException();
        }

        public bool IsReusable
        {
            get { return false; }
        }

        public IServiceLocator ServiceLocator
        {
            get { return FakeContext.Current.ServiceLocator; }
        }
    }
}