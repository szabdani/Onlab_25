using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticSharp;
using SportScheduler.Models;

namespace SportScheduler
{
	public class ScheduleChoromosome : ChromosomeBase
	{
		private Instance _instance;

		// Possible values: 16, 18, 20
		private int numberOfTeams = 16;

		// How many rounds there are
		// Possible values: 30, 34, 38
		// Because it's always 2RR
		private int numberOfSlots = 30;

		// Phased if the games are split into to 1RRs in the middle of the season
		private bool isPhased = false;

		public ScheduleChoromosome(Instance instance) : base(10)
		{
			numberOfTeams = instance.Resources.Teams.Count;
			numberOfSlots = instance.Resources.Slots.Count;
			isPhased = instance.Structure.Format.GameMode.Equals("P");

			CreateGenes();
		}

		public override Gene GenerateGene(int geneIndex)
		{
			// Gene assigned with a random slot
			int randomSlot = RandomizationProvider.Current.GetInt(0, numberOfSlots);
			return new Gene(randomSlot);
		}

		public override IChromosome CreateNew()
		{
			return new ScheduleChoromosome(_instance);
		}
	}
}
