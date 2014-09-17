using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace visualCaptcha_Nuget
{
	public class FrontendData
	{
		public List<string> values { get; set; }
		public string imageName { get; set; }
		public string imageFieldName { get; set; }
		public string audioFieldName { get; set; }

		public FrontendData()
		{
			values = new List<string>();
		}
	}
}
