using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoreanPass
{
	class AvailableGame
	{
		[JsonProperty("name")]
		public string Name { set; get; } = "";

		[JsonProperty("koreanName")]
		public string KoreanName { set; get; } = "";

		[JsonProperty("processName")]
		public string ProcessName { set; get; } = "";
	}
}
