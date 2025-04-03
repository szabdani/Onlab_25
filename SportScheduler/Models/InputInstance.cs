using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SportScheduler.Models
{
	[XmlRoot("Instance")]
	internal class InputInstance
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
}
