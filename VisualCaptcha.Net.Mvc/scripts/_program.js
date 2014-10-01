$(function() {
	var el = $('#captcha').visualCaptcha({
		imgPath: '/Content/img/',
		captcha: {
			numberOfImages: 5,
			routes: { start: "/Home/Start/", image:"/Home/Image/", audio:"/Home/Audio/" }
		},
		
	});

	// use next code for getting captcha object
	var captcha = el.data('captcha');
});