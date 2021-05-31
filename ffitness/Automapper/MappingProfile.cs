using AutoMapper;
using Ffitness.Models;
using Ffitness.Models.Stats;
using Ffitness.ViewModels;
using Ffitness.ViewModels.Stats;

namespace Ffitness.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ScheduledActivity, ScheduledActivityViewModel>();
            CreateMap<ScheduledActivityViewModel, ScheduledActivity>();
            CreateMap<Booking, BookingViewModel>();
            CreateMap<Activity, ActivityViewModel>();
            CreateMap<ActivityViewModel, Activity>();
            CreateMap<Activity, ActivityWithTrainersViewModel>();
            CreateMap<Trainer, TrainerViewModel>();
            CreateMap<TrainerViewModel, Trainer>();
            CreateMap<BookedScheduledActivity, BookedScheduledActivityViewModel>();
            CreateMap<ApplicationUser, AuthUserResponse>();
            CreateMap<ApplicationUser, ApplicationUserViewModel>().ReverseMap();
        }
    }
}