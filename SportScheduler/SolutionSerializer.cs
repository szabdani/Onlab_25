using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using SportScheduler;

public static class SolutionSerializer
{
	public static void SaveSolutionToFile(ScheduleChromosome chromosome, string filePath, string instanceName, string solutionName)
	{
		var solution = new Solution();
		var games = new Games
		{
			Matches = chromosome.GetScheduledMatches()
						.OrderBy(m => m.Slot)
						.ToList()
		};

		solution.Games = games;
		solution.MetaData = new SolutionMetaData { 
			Contributor = "Szabo Daniel", 
			InstanceName = instanceName, 
			SolutionName = solutionName, 
			Date = new SolutionDate { 
				Day = DateTime.Today.Day, 
				Month = DateTime.Today.Month, 
				Year = DateTime.Today.Year } 
		};

		var xml = SerializeSolutionToString(solution);
		var path = filePath + "/" + solutionName + ".xml";
		File.WriteAllText(path, xml, Encoding.UTF8);
	}

	private static string SerializeSolutionToString(Solution solution)
	{
		var serializer = new XmlSerializer(typeof(Solution));
		var settings = new XmlWriterSettings
		{
			Indent = true,
			Encoding = Encoding.UTF8,
			OmitXmlDeclaration = false
		};

		using var stringWriter = new StringWriter();
		using var xmlWriter = XmlWriter.Create(stringWriter, settings);
		serializer.Serialize(xmlWriter, solution);
		return stringWriter.ToString();
	}
	public static string SerializeGamesFromChromosome(ScheduleChromosome chromosome)
	{
		var games = new Games
		{
			Matches = chromosome.GetScheduledMatches()
						.OrderBy(m => m.Slot)
						.ToList()
		};

		var serializer = new XmlSerializer(typeof(Games));
		var settings = new XmlWriterSettings
		{
			Indent = true,
			Encoding = Encoding.UTF8,
			OmitXmlDeclaration = false
		};

		using var stringWriter = new StringWriter();
		using var xmlWriter = XmlWriter.Create(stringWriter, settings);
		serializer.Serialize(xmlWriter, games);
		return stringWriter.ToString();
	}
}
