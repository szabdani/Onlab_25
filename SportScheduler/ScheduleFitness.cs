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
		private int hardConstraintPenaltyMultiplier = 1000;
		private int formatPenaltyMultiplier = 10000;

		public ScheduleFitness(Instance instance)
		{
			this.instance = instance;
		}

		public double Evaluate(IChromosome chromosome)
		{
			(int, int, int) penalties = EvaluatePenaltyPoints(chromosome);
			// Invert penalty to fit GeneticSharp's maximization
			return 1.0 / (1 + EvaluatePenaltyPoints(chromosome).Item1);
		}
		public (int, int, int) EvaluatePenaltyPoints(IChromosome chromosome)
		{
			if (chromosome is not ScheduleChromosome)
				throw new ArgumentException("Chromosome must be a ScheduleChromosome");
			
			var myChromosome = chromosome as ScheduleChromosome;
			var evaluator = new ConstraintEvaluator();

			int totalPenalty = 0;
			int softPenalty = 0;
			int hardPenalty = 0;
			var matches = myChromosome.GetScheduledMatches();

			// Apply penalties for violating the double round robin format
			totalPenalty += CalculateFormatPenalties(matches)* formatPenaltyMultiplier;
			Console.WriteLine($"Format penalties: {totalPenalty}");

			// Apply penalties for each constraint
			foreach (var constraint in instance.Constraints.CapacityConstraints.CA1s)
			{
				int penalty = evaluator.EvaluateCA1(constraint, matches);
				if (constraint.Type == "HARD")
				{
					hardPenalty += penalty;
					totalPenalty += penalty * hardConstraintPenaltyMultiplier;
				}
				else if((constraint.Type == "SOFT"))
				{
					softPenalty += penalty;
					totalPenalty += penalty;
				}
			}

			foreach (var constraint in instance.Constraints.CapacityConstraints.CA2s)
			{
				int penalty = evaluator.EvaluateCA2(constraint, matches);
				if (constraint.Type == "HARD")
				{
					hardPenalty += penalty;
					totalPenalty += penalty * hardConstraintPenaltyMultiplier;
				}
				else if ((constraint.Type == "SOFT"))
				{
					softPenalty += penalty;
					totalPenalty += penalty;
				}
			}

			foreach (var constraint in instance.Constraints.CapacityConstraints.CA3s)
			{
				int penalty = evaluator.EvaluateCA3(constraint, matches, instance.Resources.Slots.Count);
				if (constraint.Type == "HARD")
				{
					hardPenalty += penalty;
					totalPenalty += penalty * hardConstraintPenaltyMultiplier;
				}
				else if ((constraint.Type == "SOFT"))
				{
					softPenalty += penalty;
					totalPenalty += penalty;
				}
			}

			foreach (var constraint in instance.Constraints.CapacityConstraints.CA4s)
			{
				int penalty = evaluator.EvaluateCA4(constraint, matches);
				if (constraint.Type == "HARD")
				{
					hardPenalty += penalty;
					totalPenalty += penalty * hardConstraintPenaltyMultiplier;
				}
				else if ((constraint.Type == "SOFT"))
				{
					softPenalty += penalty;
					totalPenalty += penalty;
				}
			}

			foreach (var constraint in instance.Constraints.GameConstraints.GA1s)
			{
				int penalty = evaluator.EvaluateGA1(constraint, matches);
				if (constraint.Type == "HARD")
				{
					hardPenalty += penalty;
					totalPenalty += penalty * hardConstraintPenaltyMultiplier;
				}
				else if ((constraint.Type == "SOFT"))
				{
					softPenalty += penalty;
					totalPenalty += penalty;
				}
			}

			foreach (var constraint in instance.Constraints.BreakConstraints.BR1s)
			{
				int penalty = evaluator.EvaluateBR1(constraint, matches);
				if (constraint.Type == "HARD")
				{
					hardPenalty += penalty;
					totalPenalty += penalty * hardConstraintPenaltyMultiplier;
				}
				else if ((constraint.Type == "SOFT"))
				{
					softPenalty += penalty;
					totalPenalty += penalty;
				}
			}

			foreach (var constraint in instance.Constraints.BreakConstraints.BR2s)
			{
				int penalty = evaluator.EvaluateBR2(constraint, matches);
				if (constraint.Type == "HARD")
				{
					hardPenalty += penalty;
					totalPenalty += penalty * hardConstraintPenaltyMultiplier;
				}
				else if ((constraint.Type == "SOFT"))
				{
					softPenalty += penalty;
					totalPenalty += penalty;
				}
			}

			foreach (var constraint in instance.Constraints.FairnessConstraints.FA2s)
			{
				int penalty = evaluator.EvaluateFA2(constraint, matches);
				if (constraint.Type == "HARD")
				{
					hardPenalty += penalty;
					totalPenalty += penalty * hardConstraintPenaltyMultiplier;
				}
				else if ((constraint.Type == "SOFT"))
				{
					softPenalty += penalty;
					totalPenalty += penalty;
				}
			}

			foreach (var constraint in instance.Constraints.SeparationConstraints.SE1s)
			{
				int penalty = evaluator.EvaluateSE1(constraint, matches);
				if (constraint.Type == "HARD")
				{
					hardPenalty += penalty;
					totalPenalty += penalty * hardConstraintPenaltyMultiplier;
				}
				else if ((constraint.Type == "SOFT"))
				{
					softPenalty += penalty;
					totalPenalty += penalty;
				}
			}

			return (totalPenalty, hardPenalty, softPenalty);
		}

		public int CalculateFormatPenalties(List<ScheduledMatch> schedule)
		{
			int totalPenalty = 0;

			var slotGroups = schedule.GroupBy(m => m.Slot)
									 .ToDictionary(g => g.Key, g => g.ToList());

			// Violation 1: Duplicate Matches (Same Home-Away Pairing)
			totalPenalty += CalculateDuplicateMatchPenalties(schedule);

			// Violation 2: Team Playing More Than Once in a Slot
			totalPenalty += CalculateTeamInSlotPenalties(slotGroups);

			// Violation 3: Number of Matches per Slot Exceeds Limit
			totalPenalty += CalculateSlotOverloadPenalties(slotGroups);

			return totalPenalty;
		}

		// Violation 1: Duplicate Matches (Same Home-Away Pairing)
		private int CalculateDuplicateMatchPenalties(List<ScheduledMatch> schedule)
		{
			int penalty = 0;
			List<ScheduledMatch> newSchedule = new List<ScheduledMatch>();

			foreach (var match in schedule)
			{
				if (newSchedule.FirstOrDefault(m => m.Home == match.Home && m.Away == match.Away) is not null)
				{
					// Duplicate match found
					penalty++;
				}
				else
				{
					newSchedule.Add(match);
				}
			}

			return penalty;
		}

		// Violation 2: Team Playing More Than Once in a Slot
		private int CalculateTeamInSlotPenalties(Dictionary<int, List<ScheduledMatch>> slotGroups)
		{
			int penalty = 0;

			foreach (var slot in slotGroups.Values)
			{
				var teamsInSlot = new HashSet<int>();

				foreach (var match in slot)
				{
					// Check if the team already plays in the slot
					if (teamsInSlot.Contains(match.Home) || teamsInSlot.Contains(match.Away))
					{
						// Team playing more than once in the slot
						penalty++;
					}
					else
					{
						teamsInSlot.Add(match.Home);
						teamsInSlot.Add(match.Away);
					}
				}
			}

			return penalty;
		}

		// Violation 3: Number of Matches is not the same for all Slots
		private int CalculateSlotOverloadPenalties(Dictionary<int, List<ScheduledMatch>> slotGroups)
		{
			int penalty = 0;
			int numberOfMatches = instance.Resources.Teams.Count * (instance.Resources.Teams.Count - 1);
			int numberOfMatchesPerSlot = numberOfMatches / instance.Resources.Slots.Count;

			foreach (var slot in slotGroups.Values)
			{
				if (slot.Count > numberOfMatchesPerSlot)
				{
					penalty += (slot.Count - numberOfMatchesPerSlot);
				}
				else if(slot.Count < numberOfMatchesPerSlot)
				{
					penalty += (numberOfMatchesPerSlot - slot.Count);
				}
			}
			return penalty;
		}
	}
}
