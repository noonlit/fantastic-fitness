using Ffitness.Data;
using FluentValidation;
using Ffitness.ViewModels;
using System;

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
		}

	}
}
