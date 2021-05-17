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
            CreateMap<Booking, BookingViewModel>();
        }
    }
}