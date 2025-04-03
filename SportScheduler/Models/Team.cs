using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SportScheduler.Models
{
	internal class Team
	{
		[XmlAttribute("id")]
		public int Id { get; set; }

		[XmlAttribute("league")]
		public int LeagueId { get; set; }

		[XmlAttribute("name")]
		public string Name { get; set; }
	}
}
