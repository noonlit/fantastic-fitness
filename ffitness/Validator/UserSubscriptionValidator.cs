using Ffitness.Data;
using Ffitness.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ffitness.Validator
{
    public class UserSubscriptionValidator : AbstractValidator<UserSubscriptionViewModel>
    {
        private readonly ApplicationDbContext _context;

        public UserSubscriptionValidator(ApplicationDbContext context)
        {
            _context = context;

            RuleFor(s => s.SubscriptionId).NotNull().WithMessage("No subscription specified.");
            RuleFor(s => s.StartTime).NotNull().GreaterThanOrEqualTo(DateTime.Today).WithMessage("The subscription must start after today.");
        }
    }
}
