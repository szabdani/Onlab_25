using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SportScheduler.Models
{
	internal class Resources
	{
		[XmlArray("Leagues")]
		[XmlArrayItem("league")]
		public List<League> Leagues { get; set; }

		[XmlArray("Teams")]
		[XmlArrayItem("team")]
		public List<Team> Teams { get; set; }

		[XmlArray("Slots")]
		[XmlArrayItem("slot")]
		public List<Slot> Slots { get; set; }
	}
}
