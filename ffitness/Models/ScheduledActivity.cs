using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ffitness.Models
{
	public class ScheduledActivity
	{
		public int Id { get; set; }

		public string Description { get; set; }

		public Activity Activity { get; set; }

		public int ActivityId { get; set; }

		public Trainer Trainer { get; set; }

		public int TrainerId { get; set; }

		private DateTime startDate;

		public DateTime StartTime { get => startDate; set => startDate = DateTime.SpecifyKind(value, DateTimeKind.Utc); }

		private DateTime endDate;

		public DateTime EndTime { get => endDate; set => endDate = DateTime.SpecifyKind(value, DateTimeKind.Utc); }

		public int Capacity { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal Price { get; set; }
	}
}
