using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SportScheduler.Models;

namespace SportScheduler
{
	public class ScheduledMatch
	{
		[XmlAttribute("home")]
		public int Home { get; set; }

		[XmlAttribute("away")]
		public int Away { get; set; }

		[XmlAttribute("slot")]
		public int Slot { get; set; }
	}

	[XmlRoot("Games")]
	public class Games
	{
		[XmlElement("ScheduledMatch")]
		public List<ScheduledMatch> Matches { get; set; } = new();

		public void AssignMatch(int homeTeamId, int awayTeamId, int slot)
		{
			Matches.Add(new ScheduledMatch
			{
				Home = homeTeamId,
				Away = awayTeamId,
				Slot = slot
			});
		}
	}
}
