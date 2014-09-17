using System.Web.Mvc;
using System.Web.Routing;

namespace visualCaptcha_DotNet
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