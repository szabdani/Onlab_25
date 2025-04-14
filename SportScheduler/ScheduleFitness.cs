using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticSharp;
using SportScheduler.Models;


namespace SportScheduler
{
	public class ScheduleFitness : IFitness
	{
		private Instance instance;

		public ScheduleFitness(Instance instance)
		{
			this.instance = instance;
		}

		public double Evaluate(IChromosome chromosome)
		{
			var myChromosome = chromosome as ScheduleChoromosome;
			var evaluator = new ConstraintEvaluator(instance);

			int totalPenalty = 0;

			// Apply penalties for each constraint
			foreach (var constraint in instance.Constraints.CapacityConstraints.CA1s)
			{
				totalPenalty += evaluator.EvaluateCA1(constraint, schedule);
			}


			// Invert penalty to fit GeneticSharp's maximization
			return 1.0 / (1.0 + totalPenalty);
		}
	}
}
