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
		private int hardConstraintMultiplier = 100;

		public ScheduleFitness(Instance instance)
		{
			this.instance = instance;
		}

		public double Evaluate(IChromosome chromosome)
		{
			var myChromosome = chromosome as ScheduleChoromosome;
			var evaluator = new ConstraintEvaluator();

			int totalPenalty = 0;
			bool hardConstraintBroken = false;

			// Apply penalties for each constraint
			foreach (var constraint in instance.Constraints.CapacityConstraints.CA1s)
			{
				int penalty = evaluator.EvaluateCA1(constraint, myChromosome.Games);

				if (constraint.Type == "HARD" && penalty > 0)
				{
					hardConstraintBroken = true;
					penalty *= hardConstraintMultiplier;
				}
					
				totalPenalty += penalty;
			}

			if (hardConstraintBroken) Console.WriteLine("At least one HARD constraint is broken.");

			// Invert penalty to fit GeneticSharp's maximization
			return 1.0 / (1.0 + totalPenalty);
		}
	}
}
