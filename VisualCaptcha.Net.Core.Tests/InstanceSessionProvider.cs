using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using visualCaptcha_Nuget;

namespace VisualCaptcha_DotNet.Tests
{
	public class InstanceSessionProvider : ISessionProvider
	{
		private readonly Dictionary<string, VisualCaptchaSession> _store = new Dictionary<string, VisualCaptchaSession>();

		public VisualCaptchaSession GetSession(string key)
		{
			if (_store.ContainsKey(key) == false)
			{
				return null;
			}
			return _store[key];
		}

		public void SetSession(string key, VisualCaptchaSession value)
		{
			_store[key] = value;
		}
	}
}
