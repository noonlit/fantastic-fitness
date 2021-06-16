using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ffitness.Models.Stats
{
	public class PopularTrainer
	{
		public int TrainerId { get; set; }

		public string TrainerName { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal OccupancyPercentage { get; set; }
	}
}
