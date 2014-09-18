using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace VisualCaptchaNet.Core.Tests
{
	[TestFixture]
    public class VisualCaptchaTests
    {
		[Test]
		public void TestGetCorrectImages()
		{
			var captcha1 = BuildVisualCaptcha();
			captcha1.Generate(3);
			var actual = captcha1.Session.FrontendData;
			Assert.That(actual.values.Count, Is.EqualTo(3));
			Assert.That(actual.imageFieldName.Length, Is.GreaterThan(0));
			Assert.That(actual.imageName.Length, Is.GreaterThan(0));
			Assert.That(actual.audioFieldName.Length, Is.GreaterThan(0));
		}

		[Test]
		public void TestSessionMethods()
		{
			var captcha1 = BuildVisualCaptcha("one");
			captcha1.Generate(3);
			captcha1.Generate(3);

			var captcha2 = BuildVisualCaptcha("two");
			captcha2.Generate(3);

			Assert.That(captcha1.Session.FrontendData.imageFieldName, Is.Not.EqualTo(captcha2.Session.FrontendData.imageFieldName));
			Assert.That(captcha1.Session.FrontendData.audioFieldName, Is.Not.EqualTo(captcha2.Session.FrontendData.audioFieldName));
			Assert.That(captcha1.Session.FrontendData.values.First(), Is.Not.EqualTo(captcha2.Session.FrontendData.values.First()));
		}

		[Test]
		public void TestUnInitialisedFrontEndDataShouldReturnNull()
		{
			var captcha = BuildVisualCaptcha();
			Assert.That(captcha.Session, Is.Null);
		}

		[Test]
		public void TestAdiosCanBeOverRidden()
		{
			var captcha = BuildVisualCaptcha();
			captcha.AudioOptions = new List<Option>{ new Option() {path = "custom.mp3", value = "custom"} };
			captcha.Generate();
			Assert.That(captcha.Session.ValidAudioOption.path, Is.EqualTo("custom.mp3"));
			
		}

		private static VisualCaptchaNet.Core.VisualCaptcha BuildVisualCaptcha(string @namespace = "test")
		{
			return new VisualCaptchaNet.Core.VisualCaptcha
				(
					new InstanceSessionProvider(),
					null,
					@namespace,
					new List<Option>()
					{
						new Option() {name = "cat", path = "cat.png"},
						new Option() {name = "car", path = "car.png"},
						new Option() {name = "pen", path = "pen.png"},
						new Option() {name = "dog", path = "dog.png"},
					},
					new List<Option>
					{
						new Option() {path = "1plus1.mp3", value = "2"},
						new Option() {path = "2plus2.mp3", value = "4"},
						new Option() {path = "3plus3.mp3", value = "6"},
					}
				);
		}
    }
}
