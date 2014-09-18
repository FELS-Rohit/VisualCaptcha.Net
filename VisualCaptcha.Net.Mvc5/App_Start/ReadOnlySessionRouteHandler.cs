using System.Web.Mvc;
using System.Web.Routing;

namespace VisualCaptchaNet.Mvc5
{
	public class ReadOnlySessionRouteHandler : IRouteHandler
	{
		public System.Web.IHttpHandler GetHttpHandler(RequestContext requestContext)
		{
			requestContext.HttpContext.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.ReadOnly);
			return new MvcHandler(requestContext);
		}
	}
}