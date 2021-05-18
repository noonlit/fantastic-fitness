using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ffitness.Models
{
	public class ScheduledActivity
	{
		[Key]
		public int Id { get; set; }

		public string Description { get; set; }

		[Required]
		public Activity Activity { get; set; }

		public int ActivityId { get; set; }

		[Required]
		public Trainer Trainer { get; set; }

		public int TrainerId { get; set; }

		private DateTime startDate;

		[Required]
		public DateTime StartTime { get => startDate; set => startDate = DateTime.SpecifyKind(value, DateTimeKind.Utc); }

		private DateTime endDate;

		[Required]
		public DateTime EndTime { get => endDate; set => endDate = DateTime.SpecifyKind(value, DateTimeKind.Utc); }

		[Required]
		public int Capacity { get; set; }

		[Required]
		[Column(TypeName = "decimal(18,2)")]
		public decimal Price { get; set; }
	}
}
