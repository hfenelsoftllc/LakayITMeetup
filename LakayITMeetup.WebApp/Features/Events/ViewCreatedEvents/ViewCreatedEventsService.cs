using LakayITMeetup.WebApp.Data.Entities;
using LakayITMeetup.WebApp.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LakayITMeetup.WebApp.Features.Events.ViewCreatedEvents
{
    public class ViewCreatedEventsService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public ViewCreatedEventsService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            this._dbContextFactory = dbContextFactory;
        }

        public async Task<List<Event>> GetCreatedEventsAsync(int organizerId)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var events = await context.Events
                    .Where(e => e.OrganizerId == organizerId)
                    .ToListAsync();
                return events;
            }
        }

        public async Task<List<EventViewModel>> GetCreatedEventsAsync()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var events = await (context.Events?.ToListAsync() ?? Task.FromResult(new List<Event>()));

                // Map Event entities to EventViewModel
                var eventViewModels = events.Select(e => new EventViewModel
                {
                    EventId = e.EventId,
                    Title = e.Title,
                    BeginDate = e.BeginDate,
                    BeginTime = e.BeginTime,
                    EndDate = e.EndDate,
                    EndTime = e.EndTime,
                    Description = e.Description,
                    Location = e.Location,
                    MeetupLink = e.MeetupLink,
                    Category = e.Category,
                    Capacity = e.Capacity,
                    OrganizerId = e.OrganizerId
                }).ToList();

                return eventViewModels;
            }
        }
    }
}
