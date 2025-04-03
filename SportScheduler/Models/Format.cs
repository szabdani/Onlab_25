using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SportScheduler.Models
{
	internal class Format
	{
		[XmlAttribute("leagueIds")]
		public int LeagueId { get; set; }
		[XmlElement("numberRoundRobin")]
		public int NumberRoundRobin { get; set; }

		[XmlElement("compactness")]
		public char Compactness { get; set; }

		[XmlElement("gameMode")]
		public char? GameMode { get; set; }
	}
}
