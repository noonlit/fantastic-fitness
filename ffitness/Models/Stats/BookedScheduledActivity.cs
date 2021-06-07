using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ffitness.Models.Stats
{
	public class BookedScheduledActivity
	{
		public string ActivityName { get; set; }

		public string TrainerName { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime EndTime { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal Price { get; set; }
		public int RemainingSpots { get; set; }
		public int BookedSpots { get; set; }
	}
}
