using System.Xml.Serialization;
using GeneticSharp;
using SportScheduler;
using SportScheduler.Models;

class Program
{
	static void Main()
	{
		string fileName = "TestInstanceDemo.xml";
		string path = Path.Combine("D:/Dani/BME/felev_8/Onlab1/repo/Onlab_25/SportScheduler/Instances", "TestInstances_V3" , fileName);

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

		var selection = new EliteSelection();
		var crossover = new UniformCrossover();
		var mutation = new ReverseSequenceMutation();
		var fitness = new ScheduleFitness(instance);
		var chromosome = new ScheduleChromosome();
		var population = new Population(50, 70, chromosome);

		var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
		ga.Termination = new GenerationNumberTermination(100);

		Console.WriteLine("GA running...");
		ga.Start();

		Console.WriteLine($"Best solution found has {ga.BestChromosome.Fitness} fitness.");
		var myChromosome = ga.BestChromosome as ScheduleChromosome;
		Console.WriteLine(SolutionSerializer.SerializeGamesFromChromosome(myChromosome));
		SolutionSerializer.SaveSolutionToFile(myChromosome, "D:/Dani/BME/felev_8/Onlab1/repo/Onlab_25/SportScheduler/Solutions", instance.MetaData.InstanceName, instance.MetaData.InstanceName+"_MySol");
	}
}

