using System.Web;
using System.Web.Mvc;

namespace VisualCaptcha.Net.Mvc
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}