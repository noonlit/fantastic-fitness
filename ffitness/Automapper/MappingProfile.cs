using AutoMapper;
using Ffitness.Models;
using Ffitness.ViewModels;

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
        }
    }
}