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

		private DateTime startDate;

		public DateTime StartTime { get => startDate; set => startDate = DateTime.SpecifyKind(value, DateTimeKind.Utc); }

		private DateTime endDate;

		public DateTime EndTime { get => endDate; set => endDate = DateTime.SpecifyKind(value, DateTimeKind.Utc); }

		[Column(TypeName = "decimal(18,2)")]
		public decimal Price { get; set; }
		public int RemainingSpots { get; set; }
		public int BookedSpots { get; set; }
	}
}
