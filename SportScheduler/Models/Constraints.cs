using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SportScheduler.Models
{
	public class Constraints
	{
		[XmlElement("CapacityConstraints")]
		public CapacityConstraints CapacityConstraints { get; set; }

		[XmlElement("GameConstraints")]
		public GameConstraints GameConstraints { get; set; }

		[XmlElement("BreakConstraints")]
		public BreakConstraints BreakConstraints { get; set; }

		[XmlElement("SeparationConstraints")]
		public SeparationConstraints SeparationConstraints { get; set; }

		[XmlElement("FairnessConstraints")]
		public FairnessConstraints FairnessConstraints { get; set; }

	}

	public class CapacityConstraints
	{
		[XmlElement("CA1")]
		public List<CA1> CA1s { get; set; }

		[XmlElement("CA2")]
		public List<CA2> CA2s { get; set; }

		[XmlElement("CA3")]
		public List<CA3> CA3s { get; set; }

		[XmlElement("CA4")]
		public List<CA4> CA4s { get; set; }
	}

	public class GameConstraints
	{
		[XmlElement("GA1")]
		public List<GA1> GA1s { get; set; }
	}

	public class BreakConstraints
	{
		[XmlElement("BR1")]
		public List<BR1> BR1s { get; set; }

		[XmlElement("BR2")]
		public List<BR2> BR2s { get; set; }
	}

	public class SeparationConstraints
	{
		[XmlElement("SE1")]
		public List<SE1> SE1s { get; set; }
	}

	public class FairnessConstraints
	{
		[XmlElement("FA2")]
		public List<FA2> FA2s { get; set; }
	}

	public class CA1
	{
		[XmlAttribute("teams")]
		public int Teams { get; set; }

		[XmlAttribute("max")]
		public int Max { get; set; }

		[XmlAttribute("min")]
		public int Min { get; set; }

		[XmlAttribute("mode")]
		public string Mode { get; set; }

		[XmlAttribute("slots")]
		public string SlotsRaw
		{
			get => string.Join(";", Slots);
			set => Slots = value.Split(';').Select(s => int.Parse(s)).ToList();
		}

		[XmlIgnore]
		public List<int> Slots { get; set; } = new();

		[XmlAttribute("type")]
		public string Type { get; set; }

		[XmlAttribute("penalty")]
		public int Penalty { get; set; }
	}

	public class CA2
	{
		[XmlAttribute("teams1")]
		public int Teams1 { get; set; }

		[XmlAttribute("teams2")]
		public string Teams2Raw
		{
			get => string.Join(";", Teams2);
			set => Teams2 = value.Split(';').Select(s => int.Parse(s)).ToList();
		}

		[XmlIgnore]
		public List<int> Teams2 { get; set; } = new();

		[XmlAttribute("max")]
		public int Max { get; set; }

		[XmlAttribute("min")]
		public int Min { get; set; }

		[XmlAttribute("mode1")]
		public string Mode1 { get; set; }

		[XmlAttribute("mode2")]
		public string Mode2 { get; set; }

		[XmlAttribute("slots")]
		public string SlotsRaw
		{
			get => string.Join(";", Slots);
			set => Slots = value.Split(';').Select(s => int.Parse(s)).ToList();
		}

		[XmlIgnore]
		public List<int> Slots { get; set; } = new();

		[XmlAttribute("type")]
		public string Type { get; set; }

		[XmlAttribute("penalty")]
		public int Penalty { get; set; }
	}
	public class CA3
	{
		[XmlAttribute("teams1")]
		public string Teams1Raw
		{
			get => string.Join(";", Teams1);
			set => Teams1 = value.Split(';').Select(s => int.Parse(s)).ToList();
		}

		[XmlIgnore]
		public List<int> Teams1 { get; set; } = new();

		[XmlAttribute("teams2")]
		public string Teams2Raw
		{
			get => string.Join(";", Teams2);
			set => Teams2 = value.Split(';').Select(s => int.Parse(s)).ToList();
		}

		[XmlIgnore]
		public List<int> Teams2 { get; set; } = new();

		[XmlAttribute("max")]
		public int Max { get; set; }

		[XmlAttribute("min")]
		public int Min { get; set; }

		[XmlAttribute("mode1")]
		public string Mode1 { get; set; }

		[XmlAttribute("intp")]
		public int Intp { get; set; }

		[XmlAttribute("mode2")]
		public string Mode2 { get; set; }

		[XmlAttribute("slots")]
		public string SlotsRaw
		{
			get => string.Join(";", Slots);
			set => Slots = value.Split(';').Select(s => int.Parse(s)).ToList();
		}

		[XmlIgnore]
		public List<int> Slots { get; set; } = new();

		[XmlAttribute("type")]
		public string Type { get; set; }

		[XmlAttribute("penalty")]
		public int Penalty { get; set; }
	}
	public class CA4
	{
		[XmlAttribute("teams1")]
		public string Teams1Raw
		{
			get => string.Join(";", Teams1);
			set => Teams1 = value.Split(';').Select(s => int.Parse(s)).ToList();
		}

		[XmlIgnore]
		public List<int> Teams1 { get; set; } = new();

		[XmlAttribute("teams2")]
		public string Teams2Raw
		{
			get => string.Join(";", Teams2);
			set => Teams2 = value.Split(';').Select(s => int.Parse(s)).ToList();
		}

		[XmlIgnore]
		public List<int> Teams2 { get; set; } = new();


		[XmlAttribute("max")]
		public int Max { get; set; }

		[XmlAttribute("min")]
		public int Min { get; set; }

		[XmlAttribute("mode1")]
		public string Mode1 { get; set; }

		[XmlAttribute("mode2")]
		public string Mode2 { get; set; }

		[XmlAttribute("slots")]
		public string SlotsRaw
		{
			get => string.Join(";", Slots);
			set => Slots = value.Split(';').Select(s => int.Parse(s)).ToList();
		}

		[XmlIgnore]
		public List<int> Slots { get; set; } = new();

		[XmlAttribute("type")]
		public string Type { get; set; }

		[XmlAttribute("penalty")]
		public int Penalty { get; set; }
	}

	public class BR1
	{
		[XmlAttribute("teams")]
		public int Teams { get; set; }

		[XmlAttribute("mode1")]
		public string Mode1 { get; set; }

		[XmlAttribute("intp")]
		public int Intp { get; set; }

		[XmlAttribute("mode2")]
		public string Mode2 { get; set; }

		[XmlAttribute("slots")]
		public string SlotsRaw
		{
			get => string.Join(";", Slots);
			set => Slots = value.Split(';').Select(s => int.Parse(s)).ToList();
		}

		[XmlIgnore]
		public List<int> Slots { get; set; } = new();

		[XmlAttribute("type")]
		public string Type { get; set; }

		[XmlAttribute("penalty")]
		public int Penalty { get; set; }
	}

	public class BR2
	{
		[XmlAttribute("teams")]
		public string TeamsRaw
		{
			get => string.Join(";", Teams);
			set => Teams = value.Split(';').Select(s => int.Parse(s)).ToList();
		}

		[XmlIgnore]
		public List<int> Teams { get; set; } = new();

		[XmlAttribute("homeMode")]
		public string HomeMode { get; set; }

		[XmlAttribute("intp")]
		public int Intp { get; set; }

		[XmlAttribute("mode2")]
		public string Mode2 { get; set; }

		[XmlAttribute("slots")]
		public string SlotsRaw
		{
			get => string.Join(";", Slots);
			set => Slots = value.Split(';').Select(s => int.Parse(s)).ToList();
		}

		[XmlIgnore]
		public List<int> Slots { get; set; } = new();

		[XmlAttribute("type")]
		public string Type { get; set; }

		[XmlAttribute("penalty")]
		public int Penalty { get; set; }
	}

	public class GA1
	{
		[XmlAttribute("meetings")]
		public string MeetingsRaw
		{
			get => string.Join(";", Meetings.Select(m => $"{m.Item1},{m.Item2}"));
			set => Meetings = value.Split(';')
								   .Select(pair => pair.Split(','))
								   .Where(parts => parts.Length == 2)
								   .Select(parts => (int.Parse(parts[0]), int.Parse(parts[1])))
								   .ToList();
		}

		[XmlIgnore]
		public List<(int, int)> Meetings { get; set; } = new();

		[XmlAttribute("max")]
		public int Max { get; set; }

		[XmlAttribute("min")]
		public int Min { get; set; }

		[XmlAttribute("slots")]
		public string SlotsRaw
		{
			get => string.Join(";", Slots);
			set => Slots = value.Split(';').Select(s => int.Parse(s)).ToList();
		}

		[XmlIgnore]
		public List<int> Slots { get; set; } = new();

		[XmlAttribute("type")]
		public string Type { get; set; }

		[XmlAttribute("penalty")]
		public int Penalty { get; set; }
	}
	public class FA2
	{
		[XmlAttribute("teams")]
		public string TeamsRaw
		{
			get => string.Join(";", Teams);
			set => Teams = value.Split(';').Select(s => int.Parse(s)).ToList();
		}

		[XmlIgnore]
		public List<int> Teams { get; set; } = new();

		[XmlAttribute("mode")]
		public string Mode { get; set; }

		[XmlAttribute("intp")]
		public int Intp { get; set; }

		[XmlAttribute("slots")]
		public string SlotsRaw
		{
			get => string.Join(";", Slots);
			set => Slots = value.Split(';').Select(s => int.Parse(s)).ToList();
		}

		[XmlIgnore]
		public List<int> Slots { get; set; } = new();

		[XmlAttribute("type")]
		public string Type { get; set; }

		[XmlAttribute("penalty")]
		public int Penalty { get; set; }
	}

	public class SE1
	{
		[XmlAttribute("teams")]
		public string TeamsRaw
		{
			get => string.Join(";", Teams);
			set => Teams = value.Split(';').Select(s => int.Parse(s)).ToList();
		}

		[XmlIgnore]
		public List<int> Teams { get; set; } = new();

		[XmlAttribute("mode1")]
		public string Mode1 { get; set; }

		[XmlAttribute("min")]
		public int Min { get; set; }

		[XmlAttribute("slots")]
		public string SlotsRaw
		{
			get => string.Join(";", Slots);
			set => Slots = value.Split(';').Select(s => int.Parse(s)).ToList();
		}

		[XmlIgnore]
		public List<int> Slots { get; set; } = new();

		[XmlAttribute("type")]
		public string Type { get; set; }

		[XmlAttribute("penalty")]
		public int Penalty { get; set; }
	}
}
