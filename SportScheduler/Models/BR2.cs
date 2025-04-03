using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SportScheduler.Models
{
	internal class BR2
	{
		[XmlAttribute("teams")]
		public List<int> Teams { get; set; }

		[XmlAttribute("homeMode")]
		public string HomeMode { get; set; }

		[XmlAttribute("intp")]
		public int Intp { get; set; }

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
