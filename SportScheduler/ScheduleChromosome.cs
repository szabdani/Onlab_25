using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
		private static int numberOfTeams;

		/// <summary>
		/// Number of matches
		/// Because it is 2RR it willbe N*(N-1)
		/// </summary>

		private static int numberOfMatches;

		/// <summary>
		/// How many rounds there are
		/// Possible values: 30, 34, 38
		/// Because it's always 2RR
		/// </summary>
		private static int numberOfSlots;

		/// <summary>
		/// How many matches are played in each slot
		/// Possible values: 8, 9, 10
		/// </summary>
		private static int numberOfMatchesPerSlot;

		/// <summary>
		/// Phased if the games are split into to 1RRs in the middle of the season
		/// </summary>
		private static bool isPhased = false;

		/// <summary>
		/// Precomputed list of all directed matches (home != away).
		/// </summary>
		private static List<ScheduledMatch> _matchList;

		public static void SetInstance(Instance instance)
		{
			_instance = instance;
			numberOfTeams = instance.Resources.Teams.Count;
			numberOfMatches = numberOfTeams * (numberOfTeams - 1);
			numberOfSlots = instance.Resources.Slots.Count;
			numberOfMatchesPerSlot = numberOfMatches / numberOfSlots;
			isPhased = instance.Structure.Format.GameMode.Equals("P");
		}

		/// <summary>
		/// total genes = number of ordered matches = N*(N-1).
		/// Each gene stores an integer slot ∈ [0, NumberOfSlots).
		/// </summary>
		public ScheduleChromosome() : base(numberOfMatches)
		{
			// Build all matches
			_matchList = new List<ScheduledMatch>(numberOfMatches);
			for (int i = 0; i < numberOfTeams; i++)
			{
				for (int j = i + 1; j < numberOfTeams; j++)
				{
					// Slot not yet picked
					_matchList.Add(new ScheduledMatch(i, j, -1));
					_matchList.Add(new ScheduledMatch(j, i, -1));
				}
			}

			// Fill initial genes
			CreateGenes();
		}

		/// <summary>
		/// Creates Genes so that no team plays twice in the same slot
		/// </summary>
		protected override void CreateGenes()
		{
			var genes = new List<Gene>();

			int rounds = numberOfTeams - 1;

			for (int round = 0; round < rounds; round++)
			{
				for (int i = 0; i < numberOfTeams / 2; i++)
				{
					int teamA = (round + i) % (numberOfTeams - 1);
					int teamB = (numberOfTeams - 1 - i + round) % (numberOfTeams - 1);
					if (i == 0)
						teamB = numberOfTeams - 1;

					// First half: assign (teamA vs teamB) to round
					var match = _matchList.FirstOrDefault(m => m.Home == teamA && m.Away == teamB);
					if (match != null)
						match.Slot = round;
					genes.Add(new Gene(match));

					// Second half: assign (teamB vs teamA) to round + rounds
					var reverseMatch = _matchList.FirstOrDefault(m => m.Home == teamB && m.Away == teamA);
					if (reverseMatch != null)
						reverseMatch.Slot = round + rounds;
					genes.Add(new Gene(reverseMatch));
				}
			}
			ReplaceGenes(0, genes.ToArray());
		}

		/// <summary>
		/// Called by GeneticSharp to create an initial random gene at position index.
		/// We simply pick a random slot for that match.
		/// </summary>
		public override Gene GenerateGene(int index)
		{
			// var slot = RandomizationProvider.Current.GetInt(0, numberOfSlots);
			// return new Gene(slot);

			throw new NotImplementedException("Use CreateGenes() to generate valid genes.");
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
			return (ScheduledMatch)GetGene(geneIndex).Value;
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
