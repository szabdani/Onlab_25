using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SportScheduler.Models
{
	
	internal class MetaData
	{
		[XmlElement("InstanceName")]
		public string InstanceName { get; set; }

		[XmlElement("DataType")]
		public string DataType { get; set; }

		[XmlElement("Contributor")]
		public string Contributor { get; set; }

		[XmlElement("Date")]
		public DateOnly Date { get; set; }
	}
}
