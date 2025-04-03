using GeneticSharp;
using SportScheduler;

class Program
{
	static void Main()
	{
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

