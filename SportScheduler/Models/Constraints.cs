using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SportScheduler.Models
{
	internal class Constraints
	{
		[XmlArray("CapacityConstraints")]
		[XmlArrayItem("CA1")]
		public List<CA1> CA1s { get; set; }

		[XmlArray("CapacityConstraints")]
		[XmlArrayItem("CA2")]
		public List<CA2> CA2s { get; set; }

		[XmlArray("CapacityConstraints")]
		[XmlArrayItem("CA3")]
		public List<CA3> CA3s { get; set; }

		[XmlArray("CapacityConstraints")]
		[XmlArrayItem("CA4")]
		public List<CA4> CA4s { get; set; }

		[XmlArray("GameConstraints")]
		[XmlArrayItem("GA1")]
		public List<GA1> GA1s { get; set; }
	}
}
