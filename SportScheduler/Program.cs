using System.Xml.Serialization;
using GeneticSharp;
using SportScheduler;
using SportScheduler.Models;

class Program
{
	static void Main()
	{
		// File names:
		// TestInstanceDemo, ITC2021_Test1, ITC2021_Test2, ITC2021_Test3, ITC2021_Test4, ITC2021_Test5
		// Longer processing time: ITC2021_Test6, ITC2021_Test7, ITC2021_Test8

		while (true)
		{
			Console.Write("Enter the name of the instance in TestInstances_V3: ");
			string input = Console.ReadLine()?.Trim();

			// Default to "TestInstanceDemo" if input is empty
			if (string.IsNullOrWhiteSpace(input))
			{
				input = "TestInstanceDemo";
			}

			// Ensure the file name ends with ".xml"
			string fileName = input.EndsWith(".xml", StringComparison.OrdinalIgnoreCase)
				? input
				: input + ".xml";

			Console.WriteLine($"Using file: {fileName}");
			string path = Path.Combine("D:/Dani/BME/felev_8/Onlab1/repo/Onlab_25/SportScheduler/Instances", "TestInstances_V3", fileName);

			if (!File.Exists(path))
			{
				Console.WriteLine("File not found: " + path);
				return;
			}

			XmlSerializer serializer = new XmlSerializer(typeof(Instance));
			using var reader = new StreamReader(path);
			var instance = (Instance)serializer.Deserialize(reader);

			Console.WriteLine("Loaded instance: " + instance.MetaData.InstanceName);
			ScheduleChromosome.SetInstance(instance);

			var selection = new TournamentSelection(4);
			var crossover = new OnePointCrossover();
			var mutation = new ReverseSequenceMutation();
			var fitness = new ScheduleFitness(instance);
			var chromosome = new ScheduleChromosome();
			var population = new Population(1000, 2000, chromosome);

			var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
			ga.Termination = new GenerationNumberTermination(30);

			Console.WriteLine("Genetic Algorithm running...\n");
			ga.Start();
			
			(int, int, int) penalties = fitness.EvaluatePenaltyPoints(ga.BestChromosome);
			Console.WriteLine($"Best solution found has {ga.BestChromosome.Fitness/1.0*100}% fitness with\n" +
				$"Total Penalties: {penalties.Item1}\n" +
				$"Hard Penalties: {penalties.Item2}\n" +
				$"Soft Penalties: {penalties.Item3}\n");

			var myChromosome = ga.BestChromosome as ScheduleChromosome;
			Console.WriteLine(SolutionSerializer.SerializeGamesFromChromosome(myChromosome));
			SolutionSerializer.SaveSolutionToFile(myChromosome, "D:/Dani/BME/felev_8/Onlab1/repo/Onlab_25/SportScheduler/Solutions", instance.MetaData.InstanceName, instance.MetaData.InstanceName + "_MySol");
			
			// Ask for next instance
			Console.WriteLine("Would you like to solve another instance? (y/n): ");
			string continueInput = Console.ReadLine()?.Trim().ToLower();
			if (continueInput != "y")
			{
				break;
			}
		}
		Console.WriteLine("Exiting program...");
	}
}

