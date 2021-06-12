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
            CreateMap<ScheduledActivity, ScheduledActivityViewModel>().ReverseMap();
            CreateMap<Booking, BookingViewModel>();
            CreateMap<Activity, ActivityViewModel>().ReverseMap();
            CreateMap<Activity, ActivityWithTrainersViewModel>().ReverseMap();
            CreateMap<Trainer, TrainerViewModel>().ReverseMap();
            CreateMap<TrainerWithActivitiesViewModel, Trainer>().ReverseMap();
            CreateMap<BookedScheduledActivity, BookedScheduledActivityViewModel>();
            CreateMap<ApplicationUser, AuthUserResponse>();
            CreateMap<ApplicationUser, ApplicationUserViewModel>().ReverseMap();
        }
    }
}