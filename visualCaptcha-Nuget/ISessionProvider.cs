namespace visualCaptcha_Nuget
{
	public interface ISessionProvider
	{
		void SetSession(string key, VisualCaptchaSession value);
		VisualCaptchaSession GetSession(string key);
	}
}
