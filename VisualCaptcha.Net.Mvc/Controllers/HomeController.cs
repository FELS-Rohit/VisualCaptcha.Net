﻿using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VisualCaptchaNet.Core;

namespace VisualCaptchaNet.Mvc.Controllers
{
	public class HomeController : Controller
	{
		private readonly VisualCaptchaNet.Core.VisualCaptcha _visualCaptcha;

		public HomeController(): this(new HttpContextSession())
		{
		}

		public HomeController(ISessionProvider sessionProvider)
		{
			var mediaPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content");
			_visualCaptcha = new VisualCaptchaNet.Core.VisualCaptcha(sessionProvider, mediaPath);
		}

		public HomeController(ISessionProvider sessionProvider, string path)
		{
			_visualCaptcha = new VisualCaptchaNet.Core.VisualCaptcha(sessionProvider, path);
		}

		public ActionResult Index()
		{
			return View();
		}

		public JsonResult Start(int numberOfImages = VisualCaptchaNet.Core.VisualCaptcha.DefaultNumberOfImages)
		{
			_visualCaptcha.Generate(numberOfImages);

			return Json(_visualCaptcha.Session.FrontendData, JsonRequestBehavior.AllowGet);
		}

		public FileStreamResult Image(int index, bool isRetina = false) //isRetina should be on querystring.
		{
			return new FileStreamResult(_visualCaptcha.StreamImage(index, isRetina), _visualCaptcha.GetImageMimeType(index, isRetina));
		}

		public FileStreamResult Audio(string type = "mp3")
		{
			Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
			Response.Cache.SetValidUntilExpires(false);
			Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			Response.Cache.SetNoStore();

			return new FileStreamResult(_visualCaptcha.StreamAudio(type), _visualCaptcha.GetAudioMimeType(type));
		}

		[HttpPost]
		public void Try()
		{
			var result = _visualCaptcha.ValidateAnswer(Request.Form);

			var queryParams = new NameValueCollection();

			switch (result)
			{
				case CaptchaState.GeneralFail:
					queryParams.Add("status", "noCaptcha");
					Response.StatusCode = (int)HttpStatusCode.NotFound;
					Response.StatusDescription = "Not Found";
					break;
				case CaptchaState.ValidImage:
					queryParams.Add("status", "validImage");
					Response.StatusCode = (int)HttpStatusCode.OK;
					break;
				case CaptchaState.FailedImage:
					queryParams.Add("status", "failedImage");
					Response.StatusCode = (int)HttpStatusCode.Forbidden;
					break;
				case CaptchaState.ValidAudio:
					queryParams.Add("status", "validAudio");
					Response.StatusCode = (int)HttpStatusCode.OK;
					break;
				case CaptchaState.FailedAudio:
					queryParams.Add("status", "failedAudio");
					Response.StatusCode = (int)HttpStatusCode.Forbidden;
					break;
				default:
					queryParams.Add("status", "failedPost");
					Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					break;
			}

			if (Request.AcceptTypes != null && Request.AcceptTypes.Any(x => x.Contains("html")))
			//was req.accepts( 'html' ) !== undefined ) 
			{
				var querystring = string.Join("&", queryParams.AllKeys.Select(key => string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(queryParams[key]))));
				Response.Redirect("/?" + querystring);
			}

			Response.End();
			
		}
	}

	public class HttpContextSession : ISessionProvider
	{
		public VisualCaptchaSession GetSession(string key)
		{
			return (VisualCaptchaSession) HttpContext.Current.Session[key];
		}

		public void SetSession(string key, VisualCaptchaSession value)
		{
			HttpContext.Current.Session[key] = value;
		}
	}
}