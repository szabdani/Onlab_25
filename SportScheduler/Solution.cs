using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportScheduler;
using System.Xml.Serialization;

namespace SportScheduler
{
	[XmlRoot("Solution")]
	public class Solution
	{
		[XmlElement("script")]
		public string Script { get; set; }

		[XmlElement("MetaData")]
		public SolutionMetaData MetaData { get; set; }

		[XmlElement("Games")]
		public Games Games { get; set; }
	}

	public class SolutionMetaData
	{
		[XmlElement("SolutionName")]
		public string SolutionName { get; set; }

		[XmlElement("InstanceName")]
		public string InstanceName { get; set; }

		[XmlElement("ObjectiveValue")]
		public ObjectiveValue ObjectiveValue { get; set; }

		[XmlElement("Contributor")]
		public string Contributor { get; set; }

		[XmlElement("Date")]
		public SolutionDate Date { get; set; }
	}

	public class ObjectiveValue
	{
		[XmlAttribute("infeasibility")]
		public int Infeasibility { get; set; } = 0;

		[XmlAttribute("objective")]
		public int Objective { get; set; } = 0;
	}

	public class SolutionDate
	{
		[XmlAttribute("day")]
		public int Day { get; set; }

		[XmlAttribute("month")]
		public int Month { get; set; }

		[XmlAttribute("year")]
		public int Year { get; set; }
	}

	public class ScheduledMatch
	{
		[XmlAttribute("home")]
		public int Home { get; set; }

		[XmlAttribute("away")]
		public int Away { get; set; }

		[XmlAttribute("slot")]
		public int Slot { get; set; }

		public ScheduledMatch() { }
		public ScheduledMatch(int home, int away, int slot)
		{
			Home = home;
			Away = away;
			Slot = slot;
		}
	}

	[XmlRoot("Games")]
	public class Games
	{
		[XmlElement("ScheduledMatch")]
		public List<ScheduledMatch> Matches { get; set; } = new();
	}
}
