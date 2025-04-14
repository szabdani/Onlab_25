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
		private int hardConstraintMultiplier = 100;
		private Instance _instance;

		public ConstraintEvaluator(Instance instance)
		{
			_instance = instance;
		}
		public int EvaluateCA1(CA1 constraint)
		{
			throw new NotImplementedException();
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
