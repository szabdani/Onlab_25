using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SportScheduler.Models
{
	internal class SE1
	{
		[XmlAttribute("teams")]
		public List<int> Teams { get; set; }

		[XmlAttribute("mode1")]
		public string Mode1 { get; set; }

		[XmlAttribute("min")]
		public int Min { get; set; }

		[XmlAttribute("slots")]
		public List<int> Slots { get; set; }

		[XmlAttribute("type")]
		public string Type { get; set; }

		[XmlAttribute("penalty")]
		public int Penalty { get; set; }
	}
}
