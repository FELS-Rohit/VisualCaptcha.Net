using System.Security.Policy;
using System.Web.Mvc;
using System.Web.Routing;

namespace visualCaptcha_DotNet
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "VisualCaptchaStart",
				url: "Home/Start/{numberOfImages}",
				defaults: new {controller = "Home", action = "Start", numberOfImages = UrlParameter.Optional}
				);

			routes.Add(
				"VisualCaptchaImages",
				new Route("Home/Image/{index}",
					new RouteValueDictionary(
						new
						{
							controller = "Home",
							action = "Image"
						}),
					new ReadOnlySessionRouteHandler()
					)
				);

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional}
				);
		}
	}
}