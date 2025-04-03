using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SportScheduler.Models
{
	internal class ObjectiveFunction
	{
		[XmlElement("Objective")]
		public Objective Objective { get; set; }
	}
}
