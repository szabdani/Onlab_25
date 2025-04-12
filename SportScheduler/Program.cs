using System.Xml.Serialization;
using GeneticSharp;
using SportScheduler;
using SportScheduler.Models;

class Program
{
	static void Main()
	{

		string fileName = "ITC2021_Test8.xml"; // Replace with your actual file name
		string path = Path.Combine("D:/Dani/BME/felev_8/Onlab1/repo/Onlab_25/SportScheduler/Instances", "TestInstances_V3" , fileName);

		XmlSerializer serializer = new XmlSerializer(typeof(Instance));
		using var reader = new StreamReader(path);
		var instance = (Instance)serializer.Deserialize(reader);

		Console.WriteLine("Loaded instance: " + instance.MetaData.InstanceName);

		var selection = new EliteSelection();
		var crossover = new OrderedCrossover();
		var mutation = new ReverseSequenceMutation();
		var fitness = new MyProblemFitness();
		var chromosome = new MyProblemChromosome();
		var population = new Population(50, 70, chromosome);

		var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
		ga.Termination = new GenerationNumberTermination(100);

		Console.WriteLine("GA running...");
		ga.Start();

		Console.WriteLine("Best solution found has {0} fitness.", ga.BestChromosome.Fitness);
	}
}

