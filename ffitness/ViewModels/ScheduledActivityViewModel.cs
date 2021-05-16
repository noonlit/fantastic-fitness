using Ffitness.Models;
using System;

namespace Ffitness.ViewModels
{
	public class ScheduledActivityViewModel
	{
		public int Id { get; set; }

		public Activity Activity { get; set; }

		public int ActivityId { get; set; }

		public Trainer Trainer { get; set; }

		public int TrainerId { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime EndTime { get; set; }

		public int Capacity { get; set; }

		public decimal Price { get; set; }
	}
}
