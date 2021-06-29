using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ffitness.ViewModels.Stats
{
	public class PopularTrainerViewModel
	{
		public int TrainerId { get; set; }

		public string TrainerName { get; set; }

		public decimal OccupancyPercentage { get; set; }
	}
}
