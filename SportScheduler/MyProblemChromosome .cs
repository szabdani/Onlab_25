using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticSharp;

namespace SportScheduler
{
	public class MyProblemChromosome : ChromosomeBase
	{
		// Change the argument value passed to base constructor to change the length 
		// of your chromosome.
		public MyProblemChromosome() : base(10)
		{
			CreateGenes();
		}

		public override Gene GenerateGene(int geneIndex)
		{
			// Generate a gene base on my problem chromosome representation.
			return new Gene();
		}

		public override IChromosome CreateNew()
		{
			return new MyProblemChromosome();
		}
	}
}
