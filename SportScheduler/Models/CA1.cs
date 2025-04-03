using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SportScheduler.Models
{
	internal class CA1
	{
		[XmlAttribute("teams")]
		public int Teams { get; set; }

		[XmlAttribute("max")]
		public int Max { get; set; }

		[XmlAttribute("min")]
		public int Min { get; set; }

		[XmlAttribute("mode")]
		public char Mode { get; set; }

		[XmlAttribute("slots")]
		public List<int> Slots { get; set; }

		[XmlAttribute("type")]
		public string Type { get; set; }

		[XmlAttribute("penalty")]
		public int Penalty { get; set; }
	}
}
