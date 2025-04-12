using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SportScheduler.Models
{
	[XmlRoot("Instance")]
	public class Instance
	{
		[XmlElement("MetaData")]
		public MetaData MetaData { get; set; }


		[XmlElement("Resources")]
		public Resources Resources { get; set; }

		[XmlElement("Structure")]
		public Structure Structure { get; set; }


		[XmlElement("Constraints")]
		public Constraints Constraints { get; set; }


		[XmlElement("ObjectiveFunction")]
		public ObjectiveFunction ObjectiveFunction { get; set; }

	}

	public class MetaData
	{
		[XmlElement("InstanceName")]
		public string InstanceName { get; set; }

		[XmlElement("DataType")]
		public string DataType { get; set; }

		[XmlElement("Contributor")]
		public string Contributor { get; set; }

		[XmlElement("Date")]
		public String Date { get; set; }
	}

	public class Structure
	{
		[XmlElement("Format")]
		public Format Format { get; set; }
	}

	public class Format
	{
		[XmlAttribute("leagueIds")]
		public int LeagueId { get; set; }
		[XmlElement("numberRoundRobin")]
		public int NumberRoundRobin { get; set; }

		[XmlElement("compactness")]
		public string Compactness { get; set; }

		[XmlElement("gameMode")]
		public string? GameMode { get; set; }
	}

	public class ObjectiveFunction
	{
		[XmlElement("Objective")]
		public Objective Objective { get; set; }
	}

	public class Objective
	{
		[XmlElement("")]
		public string Name { get; set; }

	}

}
