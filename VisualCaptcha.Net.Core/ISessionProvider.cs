namespace VisualCaptchaNet.Core
{
	public interface ISessionProvider
	{
		void SetSession(string key, VisualCaptchaSession value);
		VisualCaptchaSession GetSession(string key);
	}
}
