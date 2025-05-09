using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
			int penalty = 0;
			int numberOfGames = 0;

			switch(constraint.Mode)
			{
				case "H":
					numberOfGames = matches.Where(game => game.Home == constraint.Team && constraint.Slots.Contains(game.Slot)).Count();
					break;
				case "A":
					numberOfGames = matches.Where(game => game.Away == constraint.Team && constraint.Slots.Contains(game.Slot)).Count();
					break;
				default:
					throw new Exception("CA1 with wrong Mode");
			}

			penalty = Math.Max(0, (numberOfGames - constraint.Max));

			return penalty * constraint.Penalty;
		}

		/// <summary>
		/// "Team1" plays at most "Max" games against "Teams2" in "Slots"
		/// Mode1 = H -> Max Home games
		/// Mode1 = A -> Max Away games
		/// Mode1 = HA -> All games
		/// Penalty given for each game that exceeds "Max"
		/// </summary>
		public int EvaluateCA2(CA2 constraint, List<ScheduledMatch> matches)
		{
			int penalty = 0;
			int numberOfGames = 0;

			int countH = matches.Where(game => game.Home == constraint.Team1 && constraint.Teams2.Contains(game.Away) && constraint.Slots.Contains(game.Slot)).Count();
			int countA = matches.Where(game => game.Away == constraint.Team1 && constraint.Teams2.Contains(game.Home) && constraint.Slots.Contains(game.Slot)).Count();

			switch (constraint.Mode1)
			{
				case "H":
					numberOfGames = countH;
					break;
				case "A":
					numberOfGames = countA; 
					break;
				case "HA":
					numberOfGames = countA + countH;
					break;
				default:
					throw new Exception("CA2 with wrong Mode");
			}

			penalty = Math.Max(0, (numberOfGames - constraint.Max));

			return penalty * constraint.Penalty;
		}

		/// <summary>
		/// Each team in "Teams1" plays at most "Max" games out of "intp" against "Teams2"
		/// Mode1 = H -> Max Home games
		/// Mode1 = A -> Max Away games
		/// Mode1 = HA -> All games
		/// Penalty is the sum of the games that exceed "Max" for each "Teams1"
		/// </summary>
		public int EvaluateCA3(CA3 constraint, List<ScheduledMatch> matches, int numberOfSlots)
		{
			int penalty = 0;

			foreach (int team1 in constraint.Teams1)
			{
				var homeMatches = matches.Where(game => game.Home == team1 && constraint.Teams2.Contains(game.Away));
				var awayMatches = matches.Where(game => game.Away == team1 && constraint.Teams2.Contains(game.Home));

				// Iterate over all possible sliding windows of size intp
				for (int startSlot = 0; startSlot <= numberOfSlots - constraint.Intp; startSlot++)
				{
					int endSlot = startSlot + constraint.Intp - 1;
					int numberOfGames = 0;

					int countH = homeMatches.Where(game => game.Slot >= startSlot && game.Slot <= endSlot).Count();
					int countA = awayMatches.Where(game => game.Slot >= startSlot && game.Slot <= endSlot).Count();

					switch (constraint.Mode1)
					{
						case "H":
							numberOfGames = countH;
							break;
						case "A":
							numberOfGames = countA;
							break;
						case "HA":
							numberOfGames = countA + countH;
							break;
						default:
							throw new Exception("CA3 with wrong Mode");
					}

					if (numberOfGames > constraint.Max)
					{
						penalty += numberOfGames - constraint.Max;
					}
				}
			}

			return penalty * constraint.Penalty;
		}

		/// <summary>
		// "Teams1" plays at most "Max" games against "Teams2 as a whole"
		/// Mode1 = H -> Max Home games
		/// Mode1 = A -> Max Away games
		/// Mode1 = HA -> All games
		/// Mode2 = GLOBAL -> During "Slots"
		/// Mode2 = EVERY -> During each slot in "Slots"
		/// Penalty is given for each game that exceeds "Max"
		/// </summary>
		public int EvaluateCA4(CA4 constraint, List<ScheduledMatch> matches)
		{
			int penalty = 0;

			// Filter matches that are in the relevant time slots
			var matchesInSlots = matches.Where(m => constraint.Slots.Contains(m.Slot)).ToList();
			var homeMatches = matchesInSlots.Where(game => constraint.Teams1.Contains(game.Home) && constraint.Teams2.Contains(game.Away)).ToList();
			var awayMatches = matchesInSlots.Where(game => constraint.Teams1.Contains(game.Away) && constraint.Teams2.Contains(game.Home)).ToList();
			List<ScheduledMatch> relevantMatches = new List <ScheduledMatch>();

			switch (constraint.Mode1)
			{
				case "H":
					relevantMatches = homeMatches;
					break;
				case "A":
					relevantMatches = awayMatches;
					break;
				case "HA":
					relevantMatches = homeMatches.Concat(awayMatches).ToList();
					break;
				default:
					throw new Exception("CA4 with wrong Mode1");
			}

			int count = 0;
			switch (constraint.Mode2)
			{
				case "GLOBAL":
					count = relevantMatches.Count();
					penalty = Math.Max(0, count - constraint.Max);
					break;
				case "EVERY":
					foreach (var slot in constraint.Slots)
					{
						count = relevantMatches.Where(m => m.Slot == slot).Count();
						penalty += Math.Max(0, count - constraint.Max);
					}
					break;
				default:
					throw new Exception("CA4 with wrong Mode2");
			}

			return penalty * constraint.Penalty;
		}

		/// <summary>
		/// Meeting:= Game between Team A and B - (a,b)
		/// At least "Min" and at most "Max" "Meetings" happen in "Slots"
		/// Penalty is given for the number of games that fail the interval 
		/// </summary>
		public int EvaluateGA1(GA1 constraint, List<ScheduledMatch> matches)
		{
			int penalty = 0;
			int count = 0;

			foreach(var meeting in constraint.Meetings)
			{
				var match = matches.FirstOrDefault(m => meeting.Item1 == m.Home && meeting.Item2 == m.Away);
				if(match is not null && constraint.Slots.Contains(match.Slot))
					count++;
			}

			if (count < constraint.Min) penalty = constraint.Min - count;
			if (count > constraint.Max) penalty = count - constraint.Max;

			return penalty * constraint.Penalty;
		}

		/// <summary>
		/// Break:= If a team plays the same home-away status as previously
		/// "Team" has at most "intp" breaks
		/// Mode2 = H -> Max Home games
		/// Mode2 = A -> Max Away games
		/// Mode2 = HA -> All games
		/// Penalty is given for each break over "intp"
		/// </summary>
		public int EvaluateBR1(BR1 constraint, List<ScheduledMatch> matches)
		{
			int penalty = 0;
			int count = 0;

			var teamMatches = matches
				.Where(m => m.Home == constraint.Team || m.Away == constraint.Team)
				.OrderBy(m => m.Slot)
				.ToList();

			for (int i = 1; i < teamMatches.Count; i++)
			{
				var prev = teamMatches[i - 1];
				var curr = teamMatches[i];

				if (!constraint.Slots.Contains(curr.Slot))
					continue;

				// check for a break
				bool isHomePrev = prev.Home == constraint.Team;
				bool isHomeCurr = curr.Home == constraint.Team;
				bool isAwayPrev = prev.Away == constraint.Team;
				bool isAwayCurr = curr.Away == constraint.Team;

				bool isBreak = false;

				if (constraint.Mode2 == "H" && isHomePrev && isHomeCurr)
					isBreak = true;
				else if (constraint.Mode2 == "A" && isAwayPrev && isAwayCurr)
					isBreak = true;
				else if (constraint.Mode2 == "HA" && ((isHomePrev && isHomeCurr) || (isAwayPrev && isAwayCurr)))
					isBreak = true;

				if (isBreak)
					count++;
			}

			if (count > constraint.Intp)
				penalty = count - constraint.Intp;

			return penalty * constraint.Penalty;
		}


		/// <summary>
		/// Break:= If a team plays the same home-away status as previously
		/// The sum of breaks of all "Teams" is less than or equal to "intp"
		/// Penalty is given for each break over "intp"
		/// <summary>
		public int EvaluateBR2(BR2 constraint, List<ScheduledMatch> matches)
		{
			int penalty = 0;
			int totalBreaks = 0;

			foreach (int team in constraint.Teams)
			{
				// Get all matches involving the team, ordered by slot
				var teamMatches = matches
					.Where(m => m.Home == team || m.Away == team)
					.OrderBy(m => m.Slot)
					.ToList();

				// Check for breaks
				for (int i = 1; i < teamMatches.Count; i++)
				{
					var prev = teamMatches[i - 1];
					var curr = teamMatches[i];

					// Only check if current match is in a relevant slot
					if (!constraint.Slots.Contains(curr.Slot))
						continue;

					bool isPrevHome = prev.Home == team;
					bool isCurrHome = curr.Home == team;

					// Break = same location twice in a row (home-home or away-away)
					if ((isPrevHome && isCurrHome) || (!isPrevHome && !isCurrHome))
						totalBreaks++;
				}
			}

			if(totalBreaks > constraint.Intp)
				penalty = totalBreaks - constraint.Intp;

			return penalty * constraint.Penalty;
		}

		/// <summary>
		/// Each pair of teams in "Teams" plays at least "min" games between consecutive mutual games
		/// Penalty is given for how many games each pairing exceeds the interval
		/// </summary>
		public int EvaluateSE1(SE1 constraint, List<ScheduledMatch> matches)
		{
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
					penalty += CalculateSE1ByPairs(teamMatches, constraint.Min);
				}
			}

			return penalty * constraint.Penalty;
		}

		private int CalculateSE1ByPairs(List<ScheduledMatch> teamMatches, int minSlots)
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

		/// <summary>
		/// Each pairing of "Teams" has a difference in home games less than or equal to "intp" after each "slot"
		/// Penalty is given for the largest difference between home games over "intp" for each team pairing
		/// </summary>
		public int EvaluateFA2(FA2 constraint, List<ScheduledMatch> matches)
		{
			int penalty = 0;
			int maxSlot = constraint.Slots.Count;

			// key: teamId, value: array of length = maxSlot + 1
			Dictionary<int, int[]> homeGameCounts = new(); 

			// Initialize
			foreach (int teamId in constraint.Teams)
				homeGameCounts[teamId] = new int[maxSlot + 1];

			foreach (var match in matches)
			{
				if (homeGameCounts.ContainsKey(match.Home))
				{
					for (int s = match.Slot; s <= maxSlot; s++)
						homeGameCounts[match.Home][s]++;
				}
			}

			// Check the differences for each slot
			foreach (int slot in constraint.Slots)
			{
				foreach (int i in constraint.Teams)
				{
					foreach (int j in constraint.Teams)
					{
						// Skip non-unique pairings
						if (i >= j) continue;

						int diff = Math.Abs(homeGameCounts[i][slot] - homeGameCounts[j][slot]);
						if (diff > constraint.Intp)
						{
							penalty += diff - constraint.Intp;
						}
					}
				}
			}

			return penalty * constraint.Penalty;
		}
	}
}
