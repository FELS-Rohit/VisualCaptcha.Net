using System.Collections.Generic;

namespace VisualCaptchaNet.Core.Tests
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
