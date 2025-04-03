using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SportScheduler.Models
{
	internal class League
	{
		[XmlAttribute("id")]
		public int Id { get; set; }

		[XmlAttribute("name")]
		public string Name { get; set; }
	}
}
