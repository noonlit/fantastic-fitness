using Ffitness.Data;
using Ffitness.Models;
using Ffitness.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ffitness.Validator
{
    public class ActivityWIthTrainersValidator : AbstractValidator<ActivityWithTrainersViewModel>
    {
        private readonly ApplicationDbContext _context;

        public ActivityWIthTrainersValidator(ApplicationDbContext context)
        {
            _context = context;

            RuleFor(m => m.Name).MinimumLength(1).MaximumLength(50);
            RuleFor(m => m.Description).MinimumLength(10).MaximumLength(150).WithMessage("The description must be between 10 and 150 characters long.");
            RuleFor(m => m.Type).Must(IsAValidActivityType);
            RuleFor(m => m.DifficultyLevel).InclusiveBetween(1, 5).WithMessage("The difficulty level must be between 1 and 5.");
            RuleFor(m => m.ActivityPicture).MinimumLength(4).When(m => !string.IsNullOrEmpty(m.ActivityPicture));
            RuleFor(m => m.PrimaryColour).MinimumLength(4).MaximumLength(7).Must(IsAValidHexCode);
            RuleFor(m => m.SecondaryColour).MinimumLength(4).MaximumLength(7).Must(IsAValidHexCode);
            /*
            RuleFor(m => m.Trainers).Custom((prop, validationContext) =>
            {
                var instance = validationContext.InstanceToValidate;

                List<int> trainersToCheck = instance.Trainers.Select(t => t.Id).ToList();
                List<int> trainersForActivity = _context.Activities.Select(a => a.Trainers).Select(t => t.Id).ToArray();     //cum se extrag id-urile trainerilor?
                if (!IsATrainer(trainersForActivity, trainersToCheck))
                {
                    validationContext.AddFailure("Could not find the given trainers.");
                }
            });
            */
        }

        private bool IsAValidHexCode(string val)
        {
            int res = 0;
            if (int.TryParse(val,
                     System.Globalization.NumberStyles.HexNumber,
                     System.Globalization.CultureInfo.InvariantCulture, out res))
                return true;
            else
                return false;
        }

        private bool IsAValidActivityType(ActivityType activityType)
        {
            if (Enum.IsDefined(typeof(ActivityType), activityType) == true)
                return true;
            else
                return false;
        }

        private bool IsATrainer(List<int> trainerList, List<int> trainersToCheck)
        {
            return (trainerList.All(x => trainersToCheck.All(y => y == x)));
        }
    }
}