using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ffitness.ViewModels.Stats
{
	public class PopularActivityViewModel
	{
		public int ActivityId { get; set; }
		public string ActivityName { get; set; }
		public decimal OccupancyPercentage { get; set; }
	}
}
