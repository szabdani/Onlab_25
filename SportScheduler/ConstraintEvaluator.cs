using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportScheduler.Models;

namespace SportScheduler
{
	public class ConstraintEvaluator
	{
		/// <summary>
		/// "Team" plays at most "Max" games in "Slots"
		/// Mode = H -> Max Home games
		/// Mode = A -> Max Away games
		/// Penalty given for each game that exceeds "Max"
		/// </summary>
		public int EvaluateCA1(CA1 constraint, List<ScheduledMatch> matches)
		{
			int numberOfGames = 0;
			switch(constraint.Mode)
			{
				case "H":
					numberOfGames = matches.Where(game => game.Home == constraint.Team).Count();
					break;
				case "A":
					numberOfGames = matches.Where(game => game.Away == constraint.Team).Count();
					break;
				default:
					throw new Exception("CA1 with wrong Mode");
			}

			int penalty = (constraint.Max-numberOfGames)*constraint.Penalty;

			if(penalty < 0)
				penalty = 0;

			return penalty;
		}


		public int EvaluateCA2(CA2 constraint)
		{
			throw new NotImplementedException();
		}

		public int EvaluateCA3(CA3 constraint)
		{
			throw new NotImplementedException();
		}

		public int EvaluateCA4(CA4 constraint)
		{
			throw new NotImplementedException();
		}

		public int EvaluateGA1(GA1 constraint)
		{
			throw new NotImplementedException();
		}

		public int EvaluateBR1(BR1 constraint)
		{
			throw new NotImplementedException();
		}

		public int EvaluateBR2(BR2 constraint)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Each pair of teams in "Teams" plays at least "min" games between consecutive mutual games
		/// Penalty is given for how many games each pairing exceeds the interval
		/// </summary>
		public int EvaluateSE1(SE1 constraint, List<ScheduledMatch> matches)
		{
			int numberOfGames = 0;
			int penalty = 0;

			// Iterate over all pairs of teams
			for (int i = 0; i < constraint.Teams.Count; i++)
			{
				for (int j = i + 1; j < constraint.Teams.Count; j++)
				{
					int teamA = constraint.Teams[i];
					int teamB = constraint.Teams[j];

					// Extract all matches between teamA and teamB
					var teamMatches = matches
						.Where(m => (m.Home == teamA && m.Away == teamB) || (m.Home == teamB && m.Away == teamA))
						.OrderBy(m => m.Slot)  // Sort by the time slot to compare consecutive games
						.ToList();

					// Calculate penalty for this specific pair of teams
					penalty += CalculatePenaltyForTeamPair(teamMatches, constraint.Min);
				}
			}

			return penalty;
		}

		private int CalculatePenaltyForTeamPair(List<ScheduledMatch> teamMatches, int minSlots)
		{
			int penalty = 0;

			// Iterate through the sorted matches to check for consecutive violations
			for (int i = 1; i < teamMatches.Count; i++)
			{
				var currentMatch = teamMatches[i];
				var previousMatch = teamMatches[i - 1];

				// Calculate the number of slots between consecutive games
				int slotsBetween = currentMatch.Slot - previousMatch.Slot;

				// If the slots between the matches are less than the required 'min', apply the penalty
				if (slotsBetween < minSlots)
				{
					// Add penalty for the violation
					penalty += (minSlots - slotsBetween);
				}
			}

			return penalty;
		}

		public int EvaluateFA2(FA2 constraint)
		{
			throw new NotImplementedException();
		}
	}
}
