using Ffitness.Data;
using FluentValidation;
using Ffitness.ViewModels;
using System;
using System.Linq;

namespace Ffitness.Validator
{
	public class ScheduledActivityValidator : AbstractValidator<ScheduledActivityViewModel>
	{
		private readonly ApplicationDbContext _context;

		public ScheduledActivityValidator(ApplicationDbContext context)
		{
			_context = context;

			RuleFor(m => m.Capacity).GreaterThan(0);
			RuleFor(m => m.ActivityId).NotNull().WithMessage("You must specify an activity.");
			RuleFor(m => m.ActivityId).NotEqual(0).WithMessage("You must specify an activity.");
			RuleFor(m => m.TrainerId).NotNull().WithMessage("You must specify a trainer.");
			RuleFor(m => m.TrainerId).NotEqual(0).WithMessage("You must specify a trainer.");
			RuleFor(m => m.StartTime).NotNull();
			RuleFor(m => m.EndTime).NotNull();
			RuleFor(m => m.StartTime).LessThan(m => m.EndTime).WithMessage("The start time must be earlier than the end time.");
			RuleFor(m => m.StartTime).GreaterThan(DateTime.UtcNow).WithMessage("The start time must be in the future.");
			RuleFor(m => m.StartTime.AddMinutes(-m.TimezoneOffsetMinutes)).Must(BeWithinBusinessHours).WithMessage("Events must start within business hours (9-19).");
			RuleFor(m => m.EndTime.AddMinutes(-m.TimezoneOffsetMinutes)).Must(BeWithinBusinessHours).WithMessage("Events must end within business hours (9-19).");
			RuleFor(m => new { m.StartTime, m.EndTime }).Must(x => !HaveOverlappingEvents(x.StartTime, x.EndTime)).WithMessage("This event overlaps with a different one.");	
		}

		private bool BeWithinBusinessHours(DateTime datetime)
		{
			return datetime.Hour >= 9 && datetime.Hour < 19;
		}

		private bool HaveOverlappingEvents(DateTime startTime, DateTime endTime)
		{
			var overlappingActivitiesCount = _context.ScheduledActivities
				.Where(a => a.StartTime >= startTime && a.StartTime <= endTime || a.EndTime >= startTime && a.EndTime <= endTime)
				.Count();

			return overlappingActivitiesCount > 0;
		}
	}
}
