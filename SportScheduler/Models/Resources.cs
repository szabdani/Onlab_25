using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SportScheduler.Models
{
	public class Resources
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

	public class League
	{
		[XmlAttribute("id")]
		public int Id { get; set; }

		[XmlAttribute("name")]
		public string Name { get; set; }
	}

	public class Team
	{
		[XmlAttribute("id")]
		public int Id { get; set; }

		[XmlAttribute("league")]
		public int LeagueId { get; set; }

		[XmlAttribute("name")]
		public string Name { get; set; }
	}
	public class Slot
	{
		[XmlAttribute("id")]
		public int Id { get; set; }

		[XmlAttribute("name")]
		public string Name { get; set; }
	}

}
