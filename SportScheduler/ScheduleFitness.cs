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
		private double hardConstraintPenalty = 10000;

		public ScheduleFitness(Instance instance)
		{
			this.instance = instance;
		}

		public double Evaluate(IChromosome chromosome)
		{
			// Invert penalty to fit GeneticSharp's maximization
			return 1.0 / (1 + EvaluatePenaltyPoints(chromosome));
		}

		public double EvaluatePenaltyPoints(IChromosome chromosome)
		{
			if (chromosome is not ScheduleChromosome)
				throw new ArgumentException("Chromosome must be a ScheduleChromosome");
			
			var myChromosome = chromosome as ScheduleChromosome;
			var evaluator = new ConstraintEvaluator();

			int totalPenalty = 0;
			var matches = myChromosome.GetScheduledMatches();

			if (evaluator.ViolatesTeamDoubleBooking(matches))
				return hardConstraintPenalty;

			// Apply penalties for each constraint
			foreach (var constraint in instance.Constraints.CapacityConstraints.CA1s)
			{
				int penalty = evaluator.EvaluateCA1(constraint, matches);
				if (constraint.Type == "HARD" && penalty > 0)
					return hardConstraintPenalty;

				totalPenalty += penalty;
			}

			foreach (var constraint in instance.Constraints.CapacityConstraints.CA2s)
			{
				int penalty = evaluator.EvaluateCA2(constraint, matches);
				if (constraint.Type == "HARD" && penalty > 0)
					return hardConstraintPenalty;

				totalPenalty += penalty;
			}

			foreach (var constraint in instance.Constraints.CapacityConstraints.CA3s)
			{
				int penalty = evaluator.EvaluateCA3(constraint, matches, instance.Resources.Slots.Count);
				if (constraint.Type == "HARD" && penalty > 0)
					return hardConstraintPenalty;

				totalPenalty += penalty;
			}

			foreach (var constraint in instance.Constraints.CapacityConstraints.CA4s)
			{
				int penalty = evaluator.EvaluateCA4(constraint, matches);
				if (constraint.Type == "HARD" && penalty > 0)
					return hardConstraintPenalty;

				totalPenalty += penalty;
			}

			foreach (var constraint in instance.Constraints.GameConstraints.GA1s)
			{
				int penalty = evaluator.EvaluateGA1(constraint, matches);
				if (constraint.Type == "HARD" && penalty > 0)
					return hardConstraintPenalty;

				totalPenalty += penalty;
			}

			foreach (var constraint in instance.Constraints.BreakConstraints.BR1s)
			{
				int penalty = evaluator.EvaluateBR1(constraint, matches);
				if (constraint.Type == "HARD" && penalty > 0)
					return hardConstraintPenalty;

				totalPenalty += penalty;
			}

			foreach (var constraint in instance.Constraints.BreakConstraints.BR2s)
			{
				int penalty = evaluator.EvaluateBR2(constraint, matches);
				if (constraint.Type == "HARD" && penalty > 0)
					return hardConstraintPenalty;

				totalPenalty += penalty;
			}

			foreach (var constraint in instance.Constraints.FairnessConstraints.FA2s)
			{
				int penalty = evaluator.EvaluateFA2(constraint, matches);
				if (constraint.Type == "HARD" && penalty > 0)
					return hardConstraintPenalty;

				totalPenalty += penalty;
			}

			foreach (var constraint in instance.Constraints.SeparationConstraints.SE1s)
			{
				int penalty = evaluator.EvaluateSE1(constraint, matches);
				if (constraint.Type == "HARD" && penalty > 0)
					return hardConstraintPenalty;

				totalPenalty += penalty;
			}

			return totalPenalty;
		}
	}
}
