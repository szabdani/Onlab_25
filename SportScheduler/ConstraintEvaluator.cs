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
		// "Team" plays at most "Max" games in "Slots"
		// Mode = H -> Max Home games
		// Mode = A -> Max Away games
		// Penalty given for each game that exceeds "Max"
		public int EvaluateCA1(CA1 constraint, Games schedule)
		{
			int numberOfGames = 0;
			switch(constraint.Mode)
			{
				case "H":
					numberOfGames = schedule.Matches.Where(game => game.Home == constraint.Team).Count();
					break;
				case "A":
					numberOfGames = schedule.Matches.Where(game => game.Away == constraint.Team).Count();
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

		public int EvaluateSE1(SE1 constraint)
		{
			throw new NotImplementedException();
		}

		public int EvaluateFA2(FA2 constraint)
		{
			throw new NotImplementedException();
		}
	}
}
