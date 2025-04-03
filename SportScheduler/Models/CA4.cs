using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SportScheduler.Models
{
	internal class CA4
	{
		[XmlAttribute("teams1")]
		public List<int> Teams1 { get; set; }

		[XmlAttribute("teams2")]
		public List<int> Teams2 { get; set; }

		[XmlAttribute("max")]
		public int Max { get; set; }

		[XmlAttribute("min")]
		public int Min { get; set; }

		[XmlAttribute("mode1")]
		public string Mode1 { get; set; }

		[XmlAttribute("mode2")]
		public string Mode2 { get; set; }

		[XmlAttribute("slots")]
		public List<int> Slots { get; set; }

		[XmlAttribute("type")]
		public string Type { get; set; }

		[XmlAttribute("penalty")]
		public int Penalty { get; set; }
	}
}
