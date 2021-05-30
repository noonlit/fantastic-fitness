using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ffitness.ViewModels.Stats
{
	public class BookedScheduledActivityViewModel
	{
		public string ActivityName { get; set; }

		public string TrainerName { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime EndTime { get; set; }
		public decimal Price { get; set; }
		public int RemainingSpots { get; set; }
		public int BookedSpots { get; set; }
	}
}
