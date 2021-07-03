using Ffitness.Data;
using Ffitness.ViewModels;
using FluentValidation;

namespace Ffitness.Validator
{
    public class ApplicationUserValidator : AbstractValidator<ApplicationUserViewModel>
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserValidator(ApplicationDbContext context)
        {
            _context = context;

            RuleFor(u => u.Email).NotNull().NotEmpty();
            RuleFor(u => u.UserName).NotNull().NotEmpty();
            RuleFor(u => u.FirstName).NotNull().NotEmpty();
            RuleFor(u => u.LastName).NotNull().NotEmpty();
        }
    }
}