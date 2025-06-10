using AutoMapper;
using LakayITMeetup.WebApp.Shared.ViewModels;

namespace LakayITMeetup.WebApp.Shared
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EventViewModel, Data.Entities.Event>();
            CreateMap<Data.Entities.Event, EventViewModel>();
        }
    }
}
