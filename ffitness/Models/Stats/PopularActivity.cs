using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ffitness.Models.Stats
{
	public class PopularActivity
	{
		public int ActivityId { get; set; }
		public string ActivityName { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal OccupancyPercentage { get; set; }
	}
}
