using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticSharp;


namespace SportScheduler
{
	public class MyProblemFitness : IFitness
	{
		public double Evaluate(IChromosome chromosome)
		{
			// Evaluate the fitness of chromosome.
			return 1;
		}
	}
}
