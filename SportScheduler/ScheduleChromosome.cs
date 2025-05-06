using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticSharp;
using SportScheduler.Models;

namespace SportScheduler
{
	public class ScheduleChromosome : ChromosomeBase
	{
		private static Instance _instance;

		/// <summary>
		/// Possible values: 16, 18, 20
		/// </summary>
		private static int numberOfTeams = 16;

		/// <summary>
		/// Number of matches
		/// Because it is 2RR it willbe N*(N-1)
		/// </summary>

		private static int numberOfMatches = 16 * 15;

		/// <summary>
		/// How many rounds there are
		/// Possible values: 30, 34, 38
		/// Because it's always 2RR
		/// </summary>
		private static int numberOfSlots = 30;

		/// <summary>
		/// Phased if the games are split into to 1RRs in the middle of the season
		/// </summary>
		private static bool isPhased = false;

		/// <summary>
		/// Precomputed list of all directed matches (home != away).
		/// </summary>
		private readonly List<(int Home, int Away)> _matchList;

		public static void SetInstance(Instance instance)
		{
			_instance = instance;
			numberOfTeams = instance.Resources.Teams.Count;
			numberOfMatches = numberOfTeams * (numberOfTeams - 1);
			numberOfSlots = instance.Resources.Slots.Count;
			isPhased = instance.Structure.Format.GameMode.Equals("P");
		}

		/// <summary>
		/// total genes = number of ordered matches = N*(N-1).
		/// Each gene stores an integer slot ∈ [0, NumberOfSlots).
		/// </summary>
		public ScheduleChromosome() : base(numberOfMatches)
		{
			// Build all ordered pairs (i,j) with i != j
			_matchList = new List<(int, int)>(numberOfMatches);
			for (int i = 0; i < numberOfTeams; i++)
			{
				for (int j = 0; j < numberOfTeams; j++)
				{
					if (i == j) continue;
					_matchList.Add((i, j));
				}
			}

			// Fill initial genes
			CreateGenes();
		}

		/// <summary>
		/// Called by GeneticSharp to create an initial random gene at position index.
		/// We simply pick a random slot for that match.
		/// </summary>
		public override Gene GenerateGene(int index)
		{
			var slot = RandomizationProvider.Current.GetInt(0, numberOfSlots);
			return new Gene(slot);
		}

		/// <summary>
		/// Must return a brand‐new instance (with the same parameters).
		/// GeneticSharp uses this during crossover/mutation to create children.
		/// </summary>
		public override IChromosome CreateNew()
		{
			return new ScheduleChromosome();
		}

		/// <summary>
		/// Helper: get the scheduled match for gene i.
		/// </summary>
		public ScheduledMatch GetScheduledMatch(int geneIndex)
		{
			var (home, away) = _matchList[geneIndex];
			var slot = (int)GetGene(geneIndex).Value;
			return new ScheduledMatch(home, away, slot);
		}

		/// <summary>
		/// Helper: get the matches of the chromosome.
		/// </summary>
		public List<ScheduledMatch> GetScheduledMatches()
		{
			var scheduledMatches = new List<ScheduledMatch>(Length);

			for (int i = 0; i < Length; i++)
			{
				scheduledMatches.Add(GetScheduledMatch(i));
			}

			return scheduledMatches;
		}
	}
}
