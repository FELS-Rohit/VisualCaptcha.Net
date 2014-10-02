
namespace VisualCaptchaNet.Core
{
	public enum CaptchaState
	{
		NoCaptcha,

		ValidImage,
		FailedImage,
		ValidAudio,
		FailedAudio,

		GeneralFail,
	}
}
